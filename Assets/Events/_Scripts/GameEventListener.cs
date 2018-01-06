using UnityEngine;
using UnityEngine.Events;

namespace EventObjects {

	public class GameEventListener : MonoBehaviour {

		[SerializeField] GameEvent _triggerEvent;
		[SerializeField] UnityEvent _response;

		/// <summary>
		/// Gets/sets the triggerEvent object. Note that changing this
		/// value will cause the listener to automatically re-register.
		///
		/// Making this null effectively disables the event.
		/// </summary>
		public GameEvent triggerEvent {
			set {
				if(_triggerEvent != value) {
					TryUnregister();
					_triggerEvent = value;
					TryRegister();
				}
			}
			get { return _triggerEvent; }
		}

		/// <summary>
		/// Gets/sets the response. Making this null effectively disables
		/// the event.
		/// </summary>
		public UnityEvent response {
			set { _response = value; }
			get { return _response; }
		}

		/// <summary>
		/// Invoke the selected response. If response is set to
		/// null, then nothing happens.
		/// </summary>
		public void OnRaiseEvent() {
			if(response != null) {
				response.Invoke();
			}
		}


		#region Registration helpers

		/// <summary>
		/// Attempts to register to triggerEvent.
		/// </summary>
		private void TryRegister() {
			if(triggerEvent != null) {
				triggerEvent.RegisterListener(this);
			}
		}

		/// <summary>
		/// Attempts to unregister from triggerEvent.
		/// </summary>
		private void TryUnregister() {
			if(triggerEvent != null) {
				triggerEvent.UnregisterListener(this);
			}
		}

		#endregion


		#region Unity events

		private void OnEnable() {
			TryRegister();
		}

		private void OnDisable() {
			TryUnregister();
		}

		#endregion


	}

}
