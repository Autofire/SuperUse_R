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

		[SerializeField] float walkingSpeed = 1f;
		[SerializeField] float normalGravity = -9.8f;

		[Space(10)]
		[Tooltip("How high the character goes at the peek of a full jump. Making this zero disables jumps.")]
		[SerializeField] float jumpHeight;
		[Tooltip("How far the character goes at the peek of a full jump. Making this zero disables jumps.")]
		[SerializeField] float jumpPeekDist;

		Rigidbody rb;

		Vector3 velocity;
		float gravity;
		bool isJumping;

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
			Vector3 targetPos;

			velocity.y += gravity * Time.fixedDeltaTime;

			if(Physics.BoxCast(
				center:                  rb.position,
				halfExtents:             (boundingBox.size * (0.5f)),
				direction:               velocity,
				hitInfo:                 out hitInfo,
				orientation:             rb.rotation,
				maxDistance:             velocity.magnitude * Time.fixedDeltaTime,
				layerMask:               collisionMask,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore
				))
			{
				targetPos = rb.position + velocity.normalized * hitInfo.distance;
				velocity = Vector3.ProjectOnPlane(velocity, hitInfo.normal);
			}
			else {
				targetPos = rb.position + velocity * Time.fixedDeltaTime;
			}


			rb.MovePosition(targetPos);
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
				isJumping = true;

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