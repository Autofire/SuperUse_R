﻿using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider))]
public class SpawnField : MonoBehaviour {

	[Tooltip("Turn this on if, when the player is spawned, the camera points the wrong way.")]
	[SerializeField] private bool useRotationHack = true;
	[Tooltip("Turn this off if using some non-90 degree angles. Can break certain orientations.")]
	[SerializeField] private bool applyMyRotation = true;

	private BoxCollider box;

	private void Awake() {
		box = GetComponent<BoxCollider>();

		Assert.IsNotNull(box);
	}
		
	public Vector3 AlignPosition(Vector3 attemptedPos) {
		// TODO Make this instead clamp the player down to a location; centering them is a little weird.
		return transform.position;
		//return Vector3.ProjectOnPlane(attemptedPos, transform.forward);
	}

	public Quaternion AlignRotation(Vector3 attemptedUp) {
		// We want to rotate about the transform.forward axis
		//Vector3 attemptedUpInPlane = Vector3.ProjectOnPlane(attemptedUp, transform.forward);

		Vector3 targetUp;
		Quaternion targetRot;

		float proximityToUp = Vector3.Dot(attemptedUp, transform.up);
		float proximityToRight = Vector3.Dot(attemptedUp, transform.right);


		if(Mathf.Abs(proximityToUp) >= Mathf.Abs(proximityToRight)) {
			// We're closer to the up/down directions
			targetUp = transform.up * Mathf.Sign(proximityToUp);
		}
		else {
			targetUp = transform.right * Mathf.Sign(proximityToRight);
		}

		targetRot = Quaternion.FromToRotation(Vector3.up, targetUp);	// Want rotation from Vector3.up (the default up) into targetUp

		// HACK This probably breaks some of the more creative rotations, but it stops the camera from totally breaking
		if(useRotationHack && targetRot.x == 1.0f) {
			Debug.Log("Overriding rotation");
			targetRot = new Quaternion(0f, 0f, 1f, 0f);
		}

		// This fixes some of the more creative rotations
		if(applyMyRotation) {
			targetRot = targetRot * transform.rotation;
		}

		return targetRot;
	}
}
