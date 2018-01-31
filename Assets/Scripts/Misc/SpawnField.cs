﻿using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider))]
public class SpawnField : MonoBehaviour {

	private BoxCollider box;

	private void Awake() {
		box = GetComponent<BoxCollider>();

		Assert.IsNotNull(box);
	}
		
	public Vector3 AlignPosition(Vector3 attemptedPos) {
		// TODO Make this instead clamp the player down to a location; centering them is a little weird.
		return box.center;
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

		targetRot = Quaternion.FromToRotation(Vector3.up, targetUp);

		// HACK This probably breaks some of the more creative rotations
		if(Mathf.Approximately(targetRot.eulerAngles.x, 180f)) {
			targetRot = Quaternion.Euler(0f, 0f, 180f);
		}

		return targetRot;
	}
}