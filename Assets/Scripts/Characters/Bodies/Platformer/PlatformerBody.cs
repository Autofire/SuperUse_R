using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Bodies {

	public class PlatformerBody : BaseBody, IMoveX, IStand {

		[SerializeField] TriggerObserver footBox;
		[SerializeField] BoxCollider boundingBox;
		[SerializeField] LayerMask collisionMask = -1;

		[Space(10)]

		[SerializeField] float xSpeed = 1f;

		// TODO make these vectors
		[SerializeField] float normalGravity = -9.8f;
		[SerializeField] float jumpGravity = -4.4f;

		Rigidbody rb;

		Vector3 velocity;
		float gravity;

		#region Unity events
		override protected void Awake() {
			base.Awake();

			rb = GetComponent<Rigidbody>();

			velocity = Vector3.zero;
			gravity = normalGravity;
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


			/*
			if(IsStanding() && Mathf.Sign(velocity.y) == Mathf.Sign(gravity)) {
				// We're going to fall through the floor with 
				velocity.y = 0;
			}*/



			//rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
			rb.MovePosition(targetPos);

			//Debug.Log(velocity);
		}
		#endregion

		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			//rb.MovePosition(rb.position + xSpeed * transform.right * magnitude * Time.fixedDeltaTime);
			velocity.x = xSpeed * magnitude;
		}

		public bool IsStanding() {
			return footBox.isTriggered;
		}
		#endregion

	}

}