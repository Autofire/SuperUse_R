using UnityEngine;

public class CopyScaleToTrailRenderer : MonoBehaviour {

	[SerializeField] private Transform srcTransform;
	[SerializeField] private TrailRenderer destTrail;

	float initTrailWidth;

	void Start() {
		initTrailWidth = destTrail.widthMultiplier;

		// Immediately apply the scale change
		Update();
	}

	void Update () {
		destTrail.widthMultiplier = srcTransform.localScale.x * initTrailWidth;		
	}
}
