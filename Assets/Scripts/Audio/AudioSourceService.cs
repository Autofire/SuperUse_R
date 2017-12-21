using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceService : MonoBehaviour {

	AudioSourcePooler pooler;

	public AudioSource GetAudioSource(
		AudioSourcePooler.ObjectRepooler releaseDelegate = null)
	{
		return pooler.GetPooledObject(releaseDelegate);
	}

	protected virtual void Awake() {
		pooler = GetComponent<AudioSourcePooler>();

		if(pooler == null) {
			pooler = AudioSourcePooler.CreatePooler(gameObject);
		}

		AudioPlayer.RegisterAudioService(this);
	}
}
