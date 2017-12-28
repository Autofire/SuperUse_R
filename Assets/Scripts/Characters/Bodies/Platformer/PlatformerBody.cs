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

		Rigidbody rb;

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

			rb = GetComponent<Rigidbody>();

			velocity = Vector3.zero;
			gravity = normalGravity;
		}

		override protected void Update() {
			base.Update();



		}

		override protected void FixedUpdate() {
			base.FixedUpdate();

			RaycastHit hitInfo;
			Vector3 newOffset = Vector3.zero;

			Vector3 boxSize = boundingBox.size * 0.5f;
			boxSize *= (1 - skinFactor);

			float skinWidth = ((boundingBox.size * 0.5f).x - boxSize.x);
			float travelDistance = (velocity.magnitude * Time.fixedDeltaTime) + skinWidth;

			velocity.y += gravity * Time.fixedDeltaTime;

			if(debugCollisions) {
				ExtDebug.DrawBoxCastBox(
					origin: rb.position,
					halfExtents: boxSize,
					orientation: rb.rotation,
					direction: velocity,
					distance: travelDistance,
					color: Color.red,
					duration: 0.1f
				);

				Debug.Log("Initial velocity " + velocity.ToString("F2"));
			}

			if(Physics.BoxCast(
				center:                  rb.position,
				halfExtents:             boxSize,
				direction:               velocity,
				hitInfo:                 out hitInfo,
				orientation:             rb.rotation,
				maxDistance:             travelDistance,
				layerMask:               collisionMask,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore
				))
			{
				if(debugCollisions) {
					ExtDebug.DrawBoxCastOnHit(
						origin: rb.position,
						halfExtents: boxSize,
						orientation: rb.rotation,
						direction: velocity,
						hitInfoDistance: hitInfo.distance,
						color: Color.cyan,
						duration: 0.1f
					);

					Debug.Log(gameObject.name + " contacts at " + hitInfo.point.ToString("F2"));
				}

				newOffset += velocity.normalized * (hitInfo.distance - skinWidth);
				velocity = Vector3.ProjectOnPlane(velocity, hitInfo.normal);

				if(debugCollisions)
					Debug.Log("Corrected velocity " + velocity.ToString("F2"));
			}

			newOffset += velocity * Time.fixedDeltaTime;

			//Debug.Log(newOffset * 10000);

			rb.MovePosition(rb.position + newOffset);
			//rb.position = rb.position + newOffset;
		}
		#endregion

		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			//rb.MovePosition(rb.position + xSpeed * transform.right * magnitude * Time.fixedDeltaTime);
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