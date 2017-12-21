using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererFX : AudioFX {

	[SerializeField] Renderer target;

	public override void Play() {
		base.Play();
		target.enabled = true;
	}

	public override void Stop () {
		base.Play();
		target.enabled = false;
	}

	public override bool IsPlaying () {
		return base.IsPlaying() || target.enabled;
	}

}
