using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXCollection : IntensityFXHandler {

	[SerializeField] FXHandler[] handlers;

	bool playing = false;

	public override void Play(float intensity) {
		foreach(FXHandler handler in handlers) {
			if(handler is IntensityFXHandler) {
				((IntensityFXHandler)handler).Play(intensity);
			}
			else {
				handler.Play();
			}
		}

		playing = true;
	}

	public override void Stop() {
		foreach(FXHandler handler in handlers) {
			handler.Stop();
		}

		playing = false;
	}

	public override bool IsPlaying() {
		return playing;
	}
}
