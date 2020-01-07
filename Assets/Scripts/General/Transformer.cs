using UnityEngine;
using VariableObjects;

public class Transformer : MonoBehaviour {

	[SerializeField] private Vector3ConstReference moveSpeed;
	[SerializeField] private Vector3ConstReference rotationSpeed;
	[SerializeField] private Vector3ConstReference scaleSpeed;

	[SerializeField] private FloatConstReference randomRotationMagnitude;

	private Vector3 extraRotationSpeed;

	private void Start() {
		Vector3 randomizedDirection = new Vector3(
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f)
		).normalized;

		extraRotationSpeed = randomizedDirection * randomRotationMagnitude.constValue;
	}

	private void Update () {
		transform.position += moveSpeed.constValue * Time.deltaTime;
		//transform.Rotate(rotationSpeed.constValue * Time.deltaTime);
		transform.localRotation = transform.localRotation * Quaternion.Euler((rotationSpeed.constValue + extraRotationSpeed) * Time.deltaTime);
		transform.localScale += scaleSpeed.constValue * Time.deltaTime;
	}
}
