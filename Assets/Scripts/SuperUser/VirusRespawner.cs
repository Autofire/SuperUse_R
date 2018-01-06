using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeTime;

		[SerializeField]
		private ViveControllerAssistantReference leftAssistant;
		[SerializeField]
		private ViveControllerAssistantReference rightAssistant;

		float chargeStartTime;
		bool isCharging;

		private void Awake() {
			isCharging = false;
		}

		private void FixedUpdate() {
			if(isCharging) {
				float vibrationStrength = (Time.time - chargeStartTime) / chargeTime.constValue;
				vibrationStrength *= vibrationStrength;

				leftAssistant.value.PulseVibration(vibrationStrength);
				rightAssistant.value.PulseVibration(vibrationStrength);
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
