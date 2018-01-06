using UnityEngine;
using UnityEngine.Events;

namespace SuperUser {

	public class CollisionObserver : MonoBehaviour {

		[SerializeField]
		private bool debugCollisions;

		[Tooltip("Only invoke the events when colliding with this collider")]
		[SerializeField] private Collider target;
		[SerializeField] private UnityEvent triggerEnter;
		[SerializeField] private UnityEvent triggerExit;

		private void OnTriggerEnter(Collider other) {
			if(other == target) {
				triggerEnter.Invoke();
			}
		}

		private void OnTriggerExit(Collider other) {
			if(other == target) {
				triggerExit.Invoke();
			}
		}

		private void OnTriggerStay(Collider other) {
			if(debugCollisions) {
				Debug.Log(gameObject.name + " bumps into " + other.gameObject.name);
			}
		}
	}
}
