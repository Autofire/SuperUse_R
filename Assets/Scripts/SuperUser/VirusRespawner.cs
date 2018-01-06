using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class VirusRespawner : MonoBehaviour {

		[SerializeField]
		private FloatConstReference chargeTime;

		float chargeStartTime;

		public void BeginRespawnCharge() {
			chargeStartTime = Time.time;
		}

		public void EndRespawnCharge() {
			if(Time.time > chargeStartTime + chargeTime.constValue) {
				Debug.Log("Heroes never die!");
			}
			else {
				Debug.Log("Oops");
			}

			Debug.Log(Time.time.ToString() + " " + (chargeStartTime + chargeTime.constValue).ToString());
		}
	} // End class
} // End namespace
