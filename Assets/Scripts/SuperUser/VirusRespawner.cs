using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeDuration;

		[Header("Visual effects and haptic feedback")]
		[Tooltip("This determines how 'smooth' the charge feels.")]
		[SerializeField] private IntConstReference rampExponent;

		[Space(5)]
		[SerializeField] private GameObjectConstReference respawnEffectObject;
		[SerializeField] private FloatConstReference initScaleFactor;
		[SerializeField] private FloatConstReference finalScaleFactor;

		[Space(5)]
		[Tooltip("This should be something between 0 and 1")]
		[SerializeField] private FloatConstReference maxChargeHapticStrength;
		[Tooltip("Once the charge is maxed out, the vibration will oscillate for this long before returning to the normal amount.")]
		[SerializeField] private FloatConstReference maxChargeHapticAlertDuration;

		[Header("Object setup")]
		[SerializeField] private VirusRespawner otherRespawner;
		[SerializeField] private TransformConstReference respawnTransform;
		[SerializeField] private GameObjectConstReference respawnReadyObject;
		[SerializeField] private ViveControllerAssistantReference assistant;

		private float chargeStartTime;
		private bool isCharging;
		private bool isFullyCharged;
		private GameObject chargeObject;
		private Vector3 initChargeObjScale;


		#region Properties



		#endregion



		#region Unity events

		private void Awake() {
			isCharging = false;

			if(maxChargeHapticStrength.constValue < 0 || maxChargeHapticStrength.constValue > 1) {
				Debug.LogWarning(
					"You have maxChargeStrength in " + this.GetType().Name + " (attached to " + gameObject.name + ") "
					+ "has a value of " + maxChargeHapticStrength.constValue + " when a value between 0 and 1 was expected"
				);
			}
		}

		private void Start() {
			Assert.IsNotNull(respawnTransform.constValue);
			Assert.IsNotNull(assistant);
		}

		private void Update() {
			if(assistant.value.Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
				BeginRespawnCharge();
			}
			else if(assistant.value.Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
				EndRespawnCharge();
			}
			else if(isCharging) {
				ContinueRespawnCharge();
			}
		}

		#endregion


		#region Private methods

		private void BeginRespawnCharge() {
			isCharging = true;
			chargeStartTime = Time.time;

			chargeObject = Instantiate(
				respawnEffectObject.constValue,
				respawnTransform.constValue.position,
				respawnTransform.constValue.rotation,
				respawnTransform.constValue
			) as GameObject;

			initChargeObjScale = chargeObject.transform.localScale;

			ContinueRespawnCharge();
		}


		private void ContinueRespawnCharge() {
			float chargeTime = Time.time - chargeStartTime;

			if(!isFullyCharged && chargeTime >= chargeDuration.constValue) {
				isFullyCharged = true;
				Destroy(chargeObject);

				chargeObject = Instantiate(
					respawnReadyObject.constValue,
					respawnTransform.constValue.position,
					respawnTransform.constValue.rotation,
					respawnTransform.constValue
				) as GameObject;

				initChargeObjScale = chargeObject.transform.localScale;
			}

			ProvideHapticFeedback(chargeTime);

			chargeObject.transform.localScale =
				initChargeObjScale
				* Mathf.Lerp( initScaleFactor.constValue, finalScaleFactor.constValue, CalcRampedCharge(chargeTime) );
		}


		private void EndRespawnCharge() {
			if(Time.time > chargeStartTime + chargeDuration.constValue) {
				Debug.Log("Heroes never die!\n" + (chargeStartTime + chargeDuration.constValue - Time.time).ToString());
			}
			else {
				Debug.Log("Oops\n" + (chargeStartTime + chargeDuration.constValue - Time.time).ToString());
			}

			isCharging = false;
			isFullyCharged = false;

			Destroy(chargeObject);
		}

		private void ProvideHapticFeedback(float chargeTime) {
			if(chargeTime > chargeDuration.constValue && chargeTime < chargeDuration.constValue + maxChargeHapticAlertDuration.constValue) {
				// We've just hit the peak of the charge. We want to do a different kind of vibration.

				// We use PerlinNoise here because it gives a nice, consistent "rumble"
				assistant.value.PulseVibration(Mathf.PerlinNoise(10 * (chargeTime), 0f));
			}
			else {
				// We're charging or have finished charging for a while now.
				assistant.value.PulseVibration(Mathf.Min(CalcRampedCharge(chargeTime), maxChargeHapticStrength.constValue));
			}
		}

		private float CalcRampedCharge(float chargeTime) {
			float charge = Mathf.Clamp01(chargeTime / chargeDuration.constValue);

			float rampedCharge = 1f;

			for(int i = 0; i < rampExponent.constValue; i++) {
				rampedCharge *= charge;
			}

			return rampedCharge;
		}

		#endregion

	} // End class
} // End namespace
