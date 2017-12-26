using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Bodies {

	public class PlatformerBody : BaseBody, IMoveX {

		[SerializeField] float xSpeed = 1f;

		[SerializeField] float normalGravity = 9.8f;
		[SerializeField] float jumpGravity = 4.4f;
	
		Rigidbody rb;

		override protected void Awake() {
			base.Awake();

			rb = GetComponent<Rigidbody>();
		}

		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			//transform.position = transform.position + transform.right * magnitude;
			rb.MovePosition(rb.position + xSpeed * transform.right * magnitude * Time.fixedDeltaTime);
			//rb.AddForce(transform.right * magnitude, ForceMode.VelocityChange);
		}


	}

}