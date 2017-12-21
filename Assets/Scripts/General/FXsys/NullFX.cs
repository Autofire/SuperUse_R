using UnityEngine;
using System.Collections;

public class NullFX : FXHandler {

	// For debugging purposes
	private bool isPlaying;

	public override void Play() { 
		isPlaying = true;
	}

	public override void Stop() {
		isPlaying = false;
	}

	public override bool IsPlaying() {
		return isPlaying;
	}

}
