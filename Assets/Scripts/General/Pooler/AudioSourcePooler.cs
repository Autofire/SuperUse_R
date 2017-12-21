using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePooler : ObjectPooler<AudioSource> {

	public static AudioSourcePooler CreatePooler(
		GameObject target,
		int initPoolSize = 0,
		bool willGrow = true)
	{
		AudioSourcePooler newPooler = target.AddComponent<AudioSourcePooler>();

		newPooler.initPoolSize = initPoolSize;
		newPooler.willGrow = willGrow;

		newPooler.InitPool();

		return newPooler;
	}

	protected override AudioSource CreateObject () {
		AudioSource newSrc = gameObject.AddComponent<AudioSource>();

		newSrc.playOnAwake = false;

		return newSrc;
	}

	protected override bool IsObjectInPool(AudioSource obj) {
		return !obj.isPlaying;
	}

}
