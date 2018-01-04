using UnityEngine;
using UnityEngine.XR;

using System.Linq;

public class EnableSuperUser : MonoBehaviour {

	void Awake() {
		string[] supportedDevices = XRSettings.supportedDevices.Skip(1).ToArray();
		XRSettings.LoadDeviceByName(supportedDevices);
	}

	private void LateUpdate() {

		if(XRSettings.loadedDeviceName == "OpenVR" && XRSettings.enabled == false) {
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
