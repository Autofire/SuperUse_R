using System.Collections.Generic;
using UnityEngine;

namespace EventObjects {

	[CreateAssetMenu(menuName = "Events/GameEvent")]
	public class GameEvent : ScriptableObject {

		private List<GameEventListener> listeners = new List<GameEventListener>();

		/// <summary>
		/// Raise the event, alerting all listeners that the event has been triggered.
		/// </summary>
		public void Raise() {
			for(int i = listeners.Count - 1; i >= 0; i--) {
				listeners[i].OnRaiseEvent();
			}
		}

		/// <summary>
		/// Adds a listener to be signaled when the event is raised.
		/// </summary>
		/// <param name="listener">Listener to add.</param>
		/// <returns>True if successfully registered; false otherwise.</returns>
		public bool RegisterListener(GameEventListener listener) {
			// If this ever bogs down, begin using a HashSet in addition
			// to a list. However, this could get quite memory heavy if we
			// did it all the time.
			if(!listeners.Contains(listener)) {
				listeners.Add(listener);
				return true;
			}
			else {
				return false;
			}
		}

		/// <summary>
		/// Unregisters the listener.
		/// </summary>
		/// <param name="listener">Listener to unregister.</param>
		/// <returns>True if successfully unregistered; false otherwise.</returns>
		public bool UnregisterListener(GameEventListener listener) {
			return listeners.Remove(listener);
		}

		// If we handled args:
		//
		// Listeners without args can recieve all events
		// Listers with args can only recieve events with args
		//
		// Events with args can be sent to all listeners
		// Events without args can only be sent to listeners without args
	}

}
