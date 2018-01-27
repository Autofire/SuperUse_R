using UnityEngine;

public class CopyRendererColorToTrailColor : MonoBehaviour {

	[SerializeField] private Renderer src;
	[SerializeField] private TrailRenderer dest;
	[SerializeField] private bool copyEmission;

	void Update () {
		dest.material.color = src.material.color;

		if(copyEmission) {
			dest.material.SetColor("_EmissionColor", src.material.GetColor("_EmissionColor"));
		}
	}
}
