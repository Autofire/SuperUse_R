using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace SuperUser {

	public class ViveControllerAssistant : MonoBehaviour {

		[SerializeField]
		private SteamVR_TrackedObject trackedObj;

		public SteamVR_Controller.Device Controller {
			get { return SteamVR_Controller.Input((int)trackedObj.index); }
		}

		#region Unity events
		private void Awake() {
			OnValidate();
		}

		private void OnValidate() {
			Assert.IsNotNull(trackedObj);
		}
		#endregion

		#region Vibration functions

		/// <summary>
		/// This is a very basic pulse function, which causes the motor to vibrate for
		/// just one frame.
		/// 
		/// Calling this by itself is pretty silly; even with a the highest possible
		/// strength, it is almost impossible to notice.
		/// </summary>
		/// <param name="strength">A number from 0 to 1, wtih 1 being maximum.</param>
		public void PulseVibration(float strength) {
			Controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
		}

		/// <summary>
		/// Do a constant vibration with the specified options. Authored by
		/// https://steamcommunity.com/app/358720/discussions/0/405693392914144440/
		/// </summary>
		/// <param name="length">How long (in seconds) that the vibration lasts.</param>
		/// <param name="strength">Strength, ranging from 0 to 1.</param>
		/// <returns>Coroutine stuff.</returns>
		public void ConstantVibration(float length, float strength) {
			StartCoroutine(_ConstantVibration(length, strength));
		}
		
		/// <summary>
		/// Make the controller vibrate linearly across the given duration. The end strength
		/// is usually a fairly low value.
		/// </summary>
		/// <param name="length">Duration of the vibration (in seconds).</param>
		/// <param name="startStrength">Strength at the start of the vibration. Is between 0 and 1, inclusive.</param>
		/// <param name="endStrength">Strength at the end of the vibration. Is between 0 and 1, inclusive.</param>
		/// <returns></returns>
		public void LinearVibration(float length, float startStrength, float endStrength) {
			StartCoroutine(_LinearVibration(length, startStrength, endStrength));
		}

		#endregion

		#region Coroutines

		private IEnumerator _LinearVibration(float length, float startStrength, float endStrength) {
			for(float i = 0; i < length; i += Time.deltaTime) {
				Controller.TriggerHapticPulse(
					(ushort)Mathf.Lerp(
						0,
						3999,
						Mathf.Lerp(startStrength, endStrength, i / length)
					)
				);
				yield return null;
			}
		}

		private IEnumerator _ConstantVibration(float length, float strength) {
			for(float i = 0; i < length; i += Time.deltaTime) {
				PulseVibration(strength);
				yield return null;
			}
		}

		#endregion

	} // End class
} // End namespace
