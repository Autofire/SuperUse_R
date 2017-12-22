using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	[SerializeField] Vector3 moveAmount;
	[SerializeField] CharacterController charController;

	void FixedUpdate () {
		if(charController != null) {

			if(charController.isGrounded) {
				charController.SimpleMove(moveAmount);
			}
			else {
				charController.SimpleMove(Vector3.zero);
			}

			/*
			CollisionFlags collisionFlags = charController.Move(moveAmount);

			if (collisionFlags == CollisionFlags.None)
				print("Free floating!");

			if ((collisionFlags & CollisionFlags.Sides) != 0)
				print("Touching sides!");

			if (collisionFlags == CollisionFlags.Sides)
				print("Only touching sides, nothing else!");

			if ((collisionFlags & CollisionFlags.Above) != 0)
				print("Touching sides!");

			if (collisionFlags == CollisionFlags.Above)
				print("Only touching Ceiling, nothing else!");

			if ((collisionFlags & CollisionFlags.Below) != 0)
				print("Touching ground!");

			if (collisionFlags == CollisionFlags.Below)
				print("Only touching ground, nothing else!");
				*/
		}
		else {
			transform.position = transform.position + moveAmount;
		}

	}
}
