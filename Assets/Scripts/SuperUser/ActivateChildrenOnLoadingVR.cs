using UnityEngine;
using UnityEngine.XR;
using System.Linq;

namespace SuperUser {

	/// <summary>
	/// Attempts to load the VR device.
	/// 
	/// If successful, all immediate children are made active.
	///
	/// Disabling and then re-enabling will make it try again.
	/// </summary>
	public class ActivateChildrenOnLoadingVR : MonoBehaviour {

		[SerializeField] string targetVR = "OpenVR";

		void OnEnable() {
			string[] supportedDevices = XRSettings.supportedDevices.Skip(1).ToArray();
			XRSettings.LoadDeviceByName(supportedDevices);
		}

		private void LateUpdate() {

			if(XRSettings.loadedDeviceName == targetVR && XRSettings.enabled == false) {
				XRSettings.enabled = true;
				SteamVR.enabled = true;

				for(int i = 0; i < transform.childCount; i++) {
					transform.GetChild(i).gameObject.SetActive(true);
				}
			}
		}

		private void OnDisable() {
			for(int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(false);
			}

			XRSettings.enabled = false;
			SteamVR.enabled = false;
		}
	}
}
