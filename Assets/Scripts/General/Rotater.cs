using UnityEngine;


public class Rotater : MonoBehaviour {

	[UnityEngine.Serialization.FormerlySerializedAs("speed")]
	[SerializeField] private float rotationSpeed;
	
	void Update () {
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}
}
