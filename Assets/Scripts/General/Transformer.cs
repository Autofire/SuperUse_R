using UnityEngine;
using VariableObjects;

public class Transformer : MonoBehaviour {

	[SerializeField] private Vector3ConstReference moveSpeed;
	[SerializeField] private Vector3ConstReference rotationSpeed;
	[SerializeField] private Vector3ConstReference scaleSpeed;

	void Update () {
		transform.position += moveSpeed.constValue * Time.deltaTime;
		transform.Rotate(rotationSpeed.constValue * Time.deltaTime);
		transform.localScale += scaleSpeed.constValue * Time.deltaTime;
	}
}
