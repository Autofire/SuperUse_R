using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : FXHandler {

	[Space(10)]
	//[SerializeField] SoundCollection.PlaybackOptions soundFX;
	[SerializeField] AudioPlayer soundOptions;

	public override void Play() {
		//soundFX.Play();
		soundOptions.Play();
	}

	public override void Stop() {
		//soundFX.Stop();
		soundOptions.Stop();
	}

	public override bool IsPlaying() {
		//return soundFX.IsPlaying();
		return soundOptions.IsPlaying();
	}
}
