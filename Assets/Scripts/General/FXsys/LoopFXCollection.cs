using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopFXCollection : FXCollection {

	[Space(10)]
	[SerializeField] float loopFrequency = 1f;

	bool isLooping = false;

	public override void Play() {
		Stop();

		isLooping = true;
		StartCoroutine(Loop());
	}

	public override void Stop() {
		isLooping = false;
		base.Stop();
	}

	public override bool IsPlaying() {
		return isLooping;
	}

	IEnumerator Loop() {
		while(isLooping) {
			base.Play();
			yield return new WaitForSeconds(loopFrequency);
		}
	}
}
