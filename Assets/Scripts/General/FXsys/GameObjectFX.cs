using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFX : FXHandler {

	[Space(10)]
	[SerializeField] GameObject targetObj;

	public override void Play () {
		//base.Play();

		targetObj.SetActive(true);
	}

	public override void Stop() {
		//base.Stop();

		targetObj.SetActive(false);
	}

	public override bool IsPlaying() {
		return
			//base.IsPlaying() ||
			targetObj.activeSelf;
	}
}
