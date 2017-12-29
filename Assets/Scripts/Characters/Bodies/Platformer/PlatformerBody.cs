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

		Vector2 velocity;
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

			velocity.y += (gravity * Time.deltaTime);
			velocity = ApplyVelocity(velocity);
		}
		#endregion


		#region Helper functions

		/// <summary>
		/// Move with the given motion. This is treated as a target offset to apply, and it is not scaled according
		/// to any kind of time.
		/// </summary>
		/// <param name="motion">Offset to move by.</param>
		/// <returns>
		/// The actual distance moved. If nothing got in the way of the motion, then this value is equal to
		/// the one passed in.
		/// </returns>
		Vector3 Move(Vector3 motion) {

			Vector3 finalMotion = motion;

			Vector3 halfBoxSize = boundingBox.size * 0.5f;
			Vector3 castBoxSize = halfBoxSize * (1 - skinFactor);

			float skinWidth   = boundingBox.size.x * 0.5f - castBoxSize.x;
			float maxTravelDist = motion.magnitude + skinWidth;
			float targetTravelDist = maxTravelDist;

			if(debugCollisions) {
				// Draw the initial cast.
				ExtDebug.DrawBoxCastBox(
					origin:      transform.position,
					halfExtents: castBoxSize,
					orientation: transform.rotation,
					direction:   motion,
					distance:    maxTravelDist,
					color:       Color.red,
					duration:    debugBoxCastLifetime
				);
			}

			RaycastHit[] allHitInfo =
				Physics.BoxCastAll(
					center:                  transform.position,
					halfExtents:             castBoxSize,
					direction:               motion,
					orientation:             transform.rotation,
					maxDistance:             maxTravelDist,
					layermask:               collisionMask,
					queryTriggerInteraction: QueryTriggerInteraction.Ignore
				);

			foreach(RaycastHit hitInfo in allHitInfo) {
				// We are going to intersect with the object if we continue down the current path.

				if(debugCollisions) {
					// Draw the cast for the collision.
					ExtDebug.DrawBoxCastOnHit(
						origin:          transform.position,
						halfExtents:     castBoxSize,
						orientation:     transform.rotation,
						direction:       motion,
						hitInfoDistance: hitInfo.distance,
						color:           Color.cyan,
						duration:        debugBoxCastLifetime
					);
				}

				if(hitInfo.distance == 0 && hitInfo.point == Vector3.zero) {
					Debug.LogError(
						gameObject.name + " is stuck inside an object named " + hitInfo.collider.gameObject.name + '\n'
						+ "No movement will be made on the object until it's freed."
					);

					targetTravelDist = 0;
				}
				else if(hitInfo.distance - skinWidth < targetTravelDist) {
					targetTravelDist = hitInfo.distance - skinWidth;
				}
			}

			if(targetTravelDist != maxTravelDist) {
				finalMotion = motion.normalized * targetTravelDist;
			}

			transform.position += finalMotion;

			return finalMotion;
		}

		Vector2 ApplyVelocity(Vector2 velocity) {
			Vector3 hMotion = transform.right * velocity.x;
			Vector3 vMotion = transform.up * velocity.y;

			Vector3 hMotionFinal = Move(hMotion * Time.deltaTime) / Time.deltaTime;
			Vector3 vMotionFinal = Move(vMotion * Time.deltaTime) / Time.deltaTime;

			//Debug.Log(vMotion.ToString("F5") + vMotionFinal.ToString("F5") + new Vector2(hMotionFinal.magnitude, vMotionFinal.magnitude).ToString());

			return new Vector2(hMotionFinal.magnitude * Mathf.Sign(velocity.x), vMotionFinal.magnitude * Mathf.Sign(velocity.y));
		}

		#endregion

		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			//velocity.x = walkingSpeed * magnitude;

			Move(transform.right * (magnitude * walkingSpeed * Time.deltaTime));
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