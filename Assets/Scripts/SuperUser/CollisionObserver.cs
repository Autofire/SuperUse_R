using UnityEngine;
using UnityEngine.Events;
using EventObjects;

namespace SuperUser {

	public class CollisionObserver : MonoBehaviour {

		[SerializeField]
		private bool debugCollisions;

		[Tooltip("Only invoke the events when colliding with this collider")]
		[SerializeField] private Collider target;
		[SerializeField] private GameEvent triggerEnter;
		[SerializeField] private GameEvent triggerExit;

		private void OnTriggerEnter(Collider other) {
			if(other == target) {
				triggerEnter.Raise();
			}
		}

		private void OnTriggerExit(Collider other) {
			if(other == target) {
				triggerExit.Raise();
			}
		}

		private void OnTriggerStay(Collider other) {
			if(debugCollisions) {
				Debug.Log(gameObject.name + " bumps into " + other.gameObject.name);
			}
		}
	}
}
