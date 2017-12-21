using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFX : IntensityFXHandler {

	[SerializeField] Light target;
	[SerializeField] float baseIntensity = 1.0f;

	void Awake() {
		UnityEngine.Assertions.Assert.IsNotNull(target);
	}

	public override void Play(float intensity) {
		target.intensity = intensity * baseIntensity;
		target.enabled = true;
	}

	public override void Stop() {
		target.enabled = false;
	}

	public override bool IsPlaying() {
		return target.enabled;
	}
}
