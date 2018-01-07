using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeTime;
		[SerializeField]
		private IntConstReference rampExponent;

		[Space(10)]
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
				float charge = Mathf.Clamp01((Time.time - chargeStartTime) / chargeTime.constValue);
				float rampedCharge = 1f;

				for(int i = 0; i < rampExponent.constValue; i++) {
					rampedCharge *= charge;
				}

				leftAssistant.value.PulseVibration(rampedCharge);
				rightAssistant.value.PulseVibration(rampedCharge);
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
