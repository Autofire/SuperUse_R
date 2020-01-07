using UnityEngine;

public class CurvedMove : MonoBehaviour {

	[SerializeField] private Transform startPoint;
	[SerializeField] private Transform endPoint;
	[SerializeField] private Transform moveTarget;

	[Space(10)]
	[SerializeField] private AnimationCurve curve;
	[SerializeField] private float cycleDuration = 10;

	private float startTime;

	private void Start() {
		startTime = Time.time;
	}

	private void Update() {
		float runTime = (Time.time - startTime) / cycleDuration;

		float lerpVal = curve.Evaluate(runTime - Mathf.FloorToInt(runTime));	// Get the decimal part of the number, and then put it on the curve

		Vector3 targetPos = Vector3.Lerp(startPoint.position, endPoint.position, lerpVal);

		moveTarget.position = targetPos;
	}

}
