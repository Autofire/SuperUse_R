using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFX : ColorableComponentFXHandler {

	[SerializeField] Gradient	flashGradient;
	[SerializeField] float		flashDuration;

	IEnumerator coroutine = null;
	bool isPlaying = false;

	public override void Play() {
		// This stops a null ref. exception if played on enable.
		InitTargets();

		Stop();

		coroutine = Flash();
		StartCoroutine(coroutine);

		isPlaying = true;
	}

	public override void Stop() {
		if(coroutine != null)
			StopCoroutine(coroutine);

		// If stopped REALLY early, the check stops an exception from occuring.
		if(targetCC != null)
			targetCC.color = baseColor;

		isPlaying = false;
	}

	public override bool IsPlaying() {
		return isPlaying;
	}


	protected IEnumerator Flash() {

		float startTime = Time.time;

		while(Time.time < startTime + flashDuration) {
			float runTime = Time.time - startTime;

			targetCC.color = flashGradient.Evaluate(runTime / flashDuration);

			yield return null;
		}

		Stop();
	}


}
