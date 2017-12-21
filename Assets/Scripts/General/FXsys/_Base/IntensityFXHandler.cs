using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FXHandler which supports Play(float intensity). Most of the time,
/// Play() is equivalent to Play(1.0).
/// </summary>
public abstract class IntensityFXHandler : FXHandler {

	/// <summary>
	/// Plays the effect with the given intensity.
	/// </summary>
	/// <param name="intensity">Intensity, with 1.0 being normal intensity and 2.0 being double.</param>
	public abstract void Play(float intensity);

	/// <summary>
	/// Play the given effect with an intensity of 1.0. This can potentially loop until Stop() is run.
	/// </summary>
	public override void Play() {
		Play(1.0f);
	}

	public void Play(bool shouldPlay, float intensity) {
		if(shouldPlay) {
			Play(intensity);
		}
		else {
			Stop();
		}
	}
}
