using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeTime;

		[Space(10)]
		[Header("Haptic feedback")]
		[Tooltip("This should be something between 0 and 1")]
		[SerializeField] private FloatConstReference maxChargeStrength;
		[Tooltip("This determines how 'smooth' the charge feels.")]
		[SerializeField] private IntConstReference rampExponent;
		[Tooltip("Once the charge is maxed out, the vibration will oscillate for this long before returning to the normal amount.")]
		[SerializeField] private FloatConstReference maxChargeAlertDuration;

		[Space(10)]
		[Header("Object setup")]
		[SerializeField] private VirusRespawner otherRespawner;
		[SerializeField] private TransformConstReference respawnTransform;
		[SerializeField] private ViveControllerAssistantReference assistant;

		float chargeStartTime;
		bool isCharging;

		private void Awake() {
			isCharging = false;

			if(maxChargeStrength.constValue < 0 || maxChargeStrength.constValue > 1) {
				Debug.LogWarning(
					"You have maxChargeStrength in " + this.GetType().Name + " (attached to " + gameObject.name + ") "
					+ "has a value of " + maxChargeStrength.constValue + " when a value between 0 and 1 was expected"
				);
			}
		}

		private void Start() {
			Assert.IsNotNull(respawnTransform.constValue);
			Assert.IsNotNull(assistant);
		}

		private void FixedUpdate() {
			if(assistant.value.Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
				BeginRespawnCharge();
			}

			if(assistant.value.Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
				EndRespawnCharge();
			}

			if(isCharging) {
				if(Time.fixedTime - chargeStartTime > chargeTime.constValue && Time.fixedTime - chargeStartTime < chargeTime.constValue + maxChargeAlertDuration.constValue) {
					// We've just hit the peak of the charge. We want to do a different kind of vibration.

					// We use PerlinNoise here because it gives a nice, consistent "rumble"
					assistant.value.PulseVibration(Mathf.PerlinNoise(10 * (Time.fixedTime - chargeStartTime), 0f));
				}
				else {
					// We're charging or have finished charging for a while now.
					float charge = Mathf.Clamp01((Time.fixedTime - chargeStartTime) / chargeTime.constValue);

					float rampedCharge = 1f;

					for(int i = 0; i < rampExponent.constValue; i++) {
						rampedCharge *= charge;
					}

					assistant.value.PulseVibration(Mathf.Min(rampedCharge, maxChargeStrength.constValue));
				}
			}
		}

		public void BeginRespawnCharge() {
			isCharging = true;
			chargeStartTime = Time.time;
		}

		public void EndRespawnCharge() {
			if(Time.time > chargeStartTime + chargeTime.constValue) {
				Debug.Log("Heroes never die!\n" + (chargeStartTime + chargeTime.constValue - Time.time).ToString());
			}
			else {
				Debug.Log("Oops\n" + (chargeStartTime + chargeTime.constValue - Time.time).ToString());
			}

			isCharging = false;
		}
	} // End class
} // End namespace
