using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleParticleFX : AudioFX {

	[SerializeField] ParticleSystem[] systems;

	void Awake() {
		systems = GetComponentsInChildren<ParticleSystem>();
	}

	/// <summary>
	/// Play all particle effects.
	/// </summary>
	public override void Play() {
		base.Play();

		foreach(ParticleSystem system in systems) {
			system.Play();
		}
	}

	public override void Stop() {
		base.Stop();

		foreach(ParticleSystem system in systems) {
			system.Stop();
		}
	}

	/// <summary>
	/// Returns true if a single particle system is playing.
	/// </summary>
	/// <returns><c>true</c> if a particle system is playing; otherwise, <c>false</c>.</returns>
	public override bool IsPlaying() {
		bool playing = false;

		foreach(ParticleSystem system in systems) {
			playing = playing || system.isPlaying;
		}

		return playing || base.IsPlaying();
	}
}
