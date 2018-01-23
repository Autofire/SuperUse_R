using UnityEngine;
using UnityEngine.Events;

namespace EventObjects {

	[System.Serializable]
	public class GameEventInvoker {

		[SerializeField] GameEvent  gameEvent;
		[SerializeField] UnityEvent unityEvent;

		/// <summary>
		/// Invoke the event. Same as Raise().
		/// </summary>
		public void Invoke() {
			if(gameEvent != null) {
				gameEvent.Raise();
			}

			unityEvent.Invoke();
		}

		/// <summary>
		/// Raise the event. Same as Invoke().
		/// </summary>
		public void Raise() {
			Invoke();
		}

		/// <summary>
		/// Determines whether this instance has any events to raise.
		/// </summary>
		/// <returns><c>true</c> if this instance has events; otherwise, <c>false</c>.</returns>
		public bool HasEvents() {
			return gameEvent != null || unityEvent.GetPersistentEventCount() > 0;
		}
	}

}