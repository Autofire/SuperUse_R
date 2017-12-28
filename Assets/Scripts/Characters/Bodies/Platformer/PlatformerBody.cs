using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters.Bodies {

	public class PlatformerBody : BaseBody, IMoveX, IStand, IJump {

		[SerializeField] TriggerObserver footBox;
		[SerializeField] BoxCollider boundingBox;
		[SerializeField] LayerMask collisionMask = -1;

		[Space(10)]
		[SerializeField] float normalGravity = -9.8f;
		[Range(0f, 1f)]
		[SerializeField] float skinFactor = 0.05f;

		[Space(10)]
		[SerializeField] float walkingSpeed = 1f;
		[Tooltip("How high the character goes at the peek of a full jump. Making this zero disables jumps.")]
		[SerializeField] float jumpHeight;
		[Tooltip("How far the character goes at the peek of a full jump. Making this zero disables jumps.")]
		[SerializeField] float jumpPeekDist;

		[Space(10)]
		[SerializeField] bool debugCollisions = false;
		[SerializeField] float debugBoxCastLifetime = 0f;

		const string VECTOR3_DEBUG = "F3";

		Vector3 velocity;
		float gravity;
		//bool isJumping;

		/// <summary>
		/// Gets the magnitude of the jump velocity.
		/// </summary>
		/// <value>The jump velocity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpVelocity {
			get { return 2 * jumpHeight * walkingSpeed / jumpPeekDist; }
		}

		/// <summary>
		/// Gets the magnitude of the jump gravity.
		/// </summary>
		/// <value>The jump gravity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpGravity {
			get { return -2 * jumpHeight * walkingSpeed * walkingSpeed / (jumpPeekDist * jumpPeekDist); }
		}

		#region Unity events
		override protected void Awake() {
			base.Awake();

			velocity = Vector3.zero;
			gravity = normalGravity;
		}

		override protected void Update() {
			base.Update();

			ApplyGravity();


			// Offset from the object's current position
			System.Nullable<Vector3> targetOffset = null;	

			// This is the size of the player's box. Keep in mind that call box casts use half the bounding box.
			Vector3 halfBoxSize = boundingBox.size * 0.5f;

			// Size of the player box, used for box casts. The skin factor shrinks the box a bit.
			Vector3 castBoxSize = halfBoxSize * (1 - skinFactor);

			// This is the width, in units, of the skin.
			float skinWidth = ((boundingBox.size * 0.5f).x - castBoxSize.x);

			// This is the farthest we'll travel, assuming we won't bump into anything.
			float maxTravelDist = (velocity.magnitude * Time.deltaTime) + skinWidth;

			// All debug messages in this function get sent here. Without this, a ton of output gets spewed all over the
			// console.
			string debugOutput = "";	// TODO Make this get stripped out in the final build

			// This is the initial velocity (obviously). This is needed because we'll be tinkering with the velocity
			// variable, and we need to know what it is before the tinkering begins.
			Vector3 initVelocity = velocity;


			if(debugCollisions) {
				// Draw the initial cast.
				ExtDebug.DrawBoxCastBox(
					origin: transform.position,
					halfExtents: castBoxSize,
					orientation: transform.rotation,
					direction: initVelocity,
					distance: maxTravelDist,
					color: Color.red,
					duration: debugBoxCastLifetime
				);

				debugOutput +=
					"Initial velocity " + initVelocity.ToString(VECTOR3_DEBUG)
					+ " (Magnitude: " + initVelocity.magnitude.ToString() + ")\n";
			}

			// We must do a BoxCastAll instead of BoxCast because there may be many collisions; BoxCast only catches the
			// very first potential collision.
			foreach(RaycastHit hitInfo in
				Physics.BoxCastAll(
					center:                  transform.position,
					halfExtents:             castBoxSize,
					direction:               initVelocity,
					orientation:             transform.rotation,
					maxDistance:             maxTravelDist,
					layermask:               collisionMask,
					queryTriggerInteraction: QueryTriggerInteraction.Ignore
				))
			{
				if(hitInfo.distance == 0 && hitInfo.point == Vector3.zero) {
					// For info on these conditions and why they point out an "I'm stuck inside something" case, see
					// https://docs.unity3d.com/ScriptReference/Physics.BoxCastAll.html
					Debug.LogError(
						gameObject.name + " is stuck inside an object named " + hitInfo.collider.gameObject.name + '\n'
						+ "No force will be applied to counteract this.");

					//targetOffset = Vector3.zero;
				}
				else {
					// We are going to intersect with the object if we continue down the current path.

					if(debugCollisions) {
						// Draw the cast for the collision.
						ExtDebug.DrawBoxCastOnHit(
							origin: transform.position,
							halfExtents: castBoxSize,
							orientation: transform.rotation,
							direction: initVelocity,
							hitInfoDistance: hitInfo.distance,
							color: Color.cyan,
							duration: debugBoxCastLifetime
						);

						debugOutput += '\n' + gameObject.name + " contacts at " + hitInfo.point.ToString(VECTOR3_DEBUG) + '\n';
					}

					// Given the potential collision, this is where we think we'll end up. Within this frame, we can
					// only end up right against the surface we just collided with.
					Vector3 newOffset = initVelocity.normalized * (hitInfo.distance - skinWidth);

					// This handles having multiple collisions; in theory, the collision which causes us to move the
					// SHORTEST distance must be the safest one. Assuming nothing else is moving, we won't want to
					// travel through two solid objects. Thus, we must selected the shortest possible offset.
					if(!targetOffset.HasValue || newOffset.sqrMagnitude <= targetOffset.Value.sqrMagnitude) {
						targetOffset = newOffset;
					}

					// We'll be shifting the object over so that it's up against the thing we're bumping into.
					// Because of this, we don't need to worry about the component of the velocity
					velocity = Vector3.ProjectOnPlane(velocity, hitInfo.normal);

					if(debugCollisions) {
						debugOutput +=
							"Corrected velocity " + velocity.ToString(VECTOR3_DEBUG)
							+ " (Magnitude: " + velocity.magnitude.ToString() + ")\n"

							+ (targetOffset.Value == newOffset ? "Using" : "Not using")
							+ " offset of " + newOffset.ToString(VECTOR3_DEBUG)
							+ " (Magnitude: " + newOffset.magnitude.ToString() + ")\n";
					}
				}
			}

			// Apply the given offset, along with any motion we have left due to velocity.
			targetOffset = targetOffset.GetValueOrDefault() + velocity * Time.deltaTime;

			// Finally, apply our offset.
			transform.position = transform.position + targetOffset.Value;

			if(debugOutput != "") {
				debugOutput +=
					"\nFinal offset: " + targetOffset.Value.ToString(VECTOR3_DEBUG)
					+ " (Magnitude: " + targetOffset.Value.magnitude.ToString() + ")\n";

				Debug.Log(debugOutput);
			}
		}
		#endregion

		#region Helper functions

		void ApplyGravity() {
			velocity.y += gravity * Time.deltaTime;
		}

		#endregion

		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			velocity.x = walkingSpeed * magnitude;
		}

		public void JumpBegin ()
		{
			if(IsStanding()) {
				//isJumping = true;

				gravity = jumpGravity;
				velocity.y += jumpVelocity;
			}

		}
		public void JumpEnd ()
		{
			gravity = normalGravity;
		}

		public bool IsStanding() {
			return footBox.isTriggered;
		}
		#endregion

	}

}