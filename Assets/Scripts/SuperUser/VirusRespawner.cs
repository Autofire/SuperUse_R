using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeTime;


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
				float chargeScale = charge * charge * charge;

				leftAssistant.value.PulseVibration(chargeScale);
				rightAssistant.value.PulseVibration(chargeScale);
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
