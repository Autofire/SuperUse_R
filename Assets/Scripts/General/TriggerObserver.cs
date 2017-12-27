using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TriggerObserver : MonoBehaviour {

	/// <summary>
	/// Returns if something is in the trigger area.
	/// </summary>
	/// <value><c>true</c> if is triggered; otherwise, <c>false</c>.</value>
	[HideInInspector] public bool isTriggered { private set; get; }
		// (More likely to be true after reading contriversial topic.)

	private bool triggeredThisFrame;

	void OnTriggerStay(Collider other) {
		isTriggered = true;
		triggeredThisFrame = true;
	}

	void FixedUpdate() {
		if(triggeredThisFrame) {
			triggeredThisFrame = false;
		}
		else {
			isTriggered = false;
		}

		Debug.Log(isTriggered);
	}
}
