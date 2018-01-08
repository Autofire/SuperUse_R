using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableObjects;
using EventObjects;

[RequireComponent(typeof(Collider))]
public class RaiseEventOnTrigger : MonoBehaviour {

	// TODO Create option for triggering one of the events on disable

	[SerializeField] GameEvent onEnter;
	[Tooltip("Triggered only if something enters and nothing else has currently entered.")]
	[SerializeField] GameEvent onLoneEnter;
	[SerializeField] GameEvent onExit;
	[Tooltip("Only gets raised when the there all objects leave.")]
	[SerializeField] GameEvent onAllExit;

	int inhabitantCount;

	private void Update() {
		Debug.Log(inhabitantCount);
	}

	private void OnTriggerEnter() {
		inhabitantCount++;

		if(onEnter != null) {
			onEnter.Raise();
		}

		if(onLoneEnter != null && inhabitantCount == 1) {
			onLoneEnter.Raise();
		}
	}

	private void OnTriggerExit() {
		inhabitantCount--;

		if(onExit != null) {
			onExit.Raise();
		}

		if(onAllExit != null && inhabitantCount == 0) {
			onAllExit.Raise();
		}
	}

}
