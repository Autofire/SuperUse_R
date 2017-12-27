using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TriggerObserver : MonoBehaviour {

	public event Action<Collider> TriggerEnter;
	public event Action<Collider> TriggerStay;
	public event Action<Collider> TriggerExit;

	/// <summary>
	/// Returns if something is in the trigger area.
	/// </summary>
	/// <value><c>true</c> if is triggered; otherwise, <c>false</c>.</value>
	[HideInInspector] public bool isTriggered { private set; get; }
		// (More likely to be true after reading contriversial topic.)

	private bool triggeredThisFrame;

	void OnTriggerEnter(Collider other) {
		if(TriggerEnter != null) {
			TriggerEnter(other);
		}
	}

	void OnTriggerStay(Collider other) {
		isTriggered = true;
		triggeredThisFrame = true;

		if(TriggerStay != null) {
			TriggerStay(other);
		}
	}

	void OnTriggerExit(Collider other) {
		if(TriggerExit != null) {
			TriggerExit(other);
		}
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
