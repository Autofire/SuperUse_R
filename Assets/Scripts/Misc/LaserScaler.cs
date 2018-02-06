using UnityEngine;
using VariableObjects;

public class LaserScaler : MonoBehaviour {

	[SerializeField] private LayerMaskConstReference layerMask;
	[SerializeField] private FloatConstReference maxLength;
	[SerializeField] private Transform scaleTarget;

	private void OnEnable() {
		AdjustScale();
		transform.hasChanged = false;
	}

	private void Update() {
		if(transform.hasChanged) {
			AdjustScale();
			transform.hasChanged = false;
		}
	}


	private void AdjustScale() {

		RaycastHit hitInfo;
		float length;

		if(Physics.Raycast(transform.position, transform.up, out hitInfo, maxLength.constValue, layerMask.constValue)) {
			length = hitInfo.distance;
		}
		else {
			length = maxLength.constValue;
		}

		scaleTarget.position = transform.position + transform.up * (length / 2);
		scaleTarget.localScale = new Vector3(scaleTarget.localScale.x, length / 2, scaleTarget.localScale.z);

	}
}
