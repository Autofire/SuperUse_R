using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioPlayer {

	// TODO Make this have multiple audio sources for when playing the same sound
	// many times.

	#region Static things
	private static AudioSourceService audioService = null;

	public static void RegisterAudioService(AudioSourceService newService) {
		audioService = newService;
	}
	#endregion

	#region Constructors
	public AudioPlayer() : this(null) { }
	public AudioPlayer(AudioClip targetClip) {
		clip = targetClip;
	}
	#endregion

	#region Playback settings
	[Tooltip( "Clip to play. If null, nothing is ever played.")]
	[SerializeField] private AudioClip _clip = (AudioClip) null;
	public AudioClip clip {
		get { return _clip; }
		set { _clip = value; }
	}

	[Tooltip("Mixer to play the sound on.")]
	[SerializeField] private AudioMixerGroup _mixerGroup = (AudioMixerGroup) null;
	public AudioMixerGroup mixerGroup {
		get { return _mixerGroup; }
		set { _mixerGroup = value; }
	}

	[Tooltip(
		"If true, then a new sound is played alongside the original. Otherwise, " +
		"the sound only plays if the previous one is not playing."
	)]
	[SerializeField] private bool _allowOverlap = true;
	public bool allowOverlap {
		get { return _allowOverlap; }
		set { _allowOverlap = value; }
	}

	[Range(0, 256)]
	[SerializeField] private int _priority = 128;
	public int priority {
		get { return _priority; }
		set { _priority = Mathf.Clamp(value, 0, 256); }
	}

	/// <summary>
	/// The clip's volume; should be between 0.0 and 1.0, inclusive. Defaults to 1.0.
	/// </summary>
	[Range(0f, 1f)]
	[SerializeField] private float _volume = 1f;
	public float volume {
		get { return _volume; }
		set { _volume = Mathf.Clamp(value, 0f, 1f); }
	}

	[Tooltip("Normal pitch level for playing clip.")]
	[Range(-3f, 3f)]
	[SerializeField] private float _pitch = 1f;
	public float pitch {
		get { return _pitch; }
		set { _pitch = Mathf.Clamp(value, -3f, 3f); }
	}

	[Tooltip(
		"Maximum delta pitch; pitch will vary randomly above an below " +
		"basePitch according to this. Keep in mind that the final pitch is " +
		"capped between -3 and 3.\n\n" +
		"Set to 0 for no change."
	)]
	[Range(0f, 6f)]
	[SerializeField] private float _pitchRange = 0f;
	public float pitchRange {
		get { return _pitchRange; }
		set { _pitchRange = Mathf.Clamp(value, 0f, 6f); }
	}

	#endregion

	#region Private variables
	//AudioSource currentSource = null;
	private List<AudioSource> currentSources = new List<AudioSource>();
	#endregion

	#region Playback control methods
	/// <summary>
	/// Play the sound, assuming clip is not null.
	/// </summary>
	public void Play() {
		if(clip != null) {
			AudioSource currentSource = GetAvailableSource();

			// If we can help it, we'll get another AudioSource.
			// Note that if we don't allow overlap, then we'll pass along a
			// audioSource that's currently playing, and then the code below will
			// reject it.
			if(currentSource == null && allowOverlap) {
				// Other source is unavailable/busy, so get a new one.
				currentSource = GetNewAudioSource();
			}

			// We must NEVER play over a source that's currently playing because
			// it seems to cause a cracking.
			if(currentSource != null && !currentSource.isPlaying) {
				RandomizeAudioSource(currentSource);
				currentSource.Play();
			}

		} // End if(clip != null)
	}

	/// <summary>
	/// Stop all instances of the sound played from this AudioPlayer.
	/// </summary>
	public void Stop() {
		//Debug.Log(currentSources.Count);

		foreach(AudioSource src in currentSources) {
			if(src != null) {
				src.Stop();
			}
		}
	}

	/// <summary>
	/// Check if the the sound sound is playing.
	/// If the clip is null, then false is always returned.
	/// </summary>
	public bool IsPlaying() {
		foreach(AudioSource src in currentSources) {
			if(src != null && src.isPlaying) {
				return true;
			}
		}

		return false;
	}
	#endregion


	#region Private methods
	/// <summary>
	/// Find an available source out of currentSources. If we can overlap, the
	/// returned source is garunteed to not be playing. If we cannot overlap, than
	/// the current source may be playing.
	/// </summary>
	/// <returns>The available source.</returns>
	private AudioSource GetAvailableSource() {
		if(allowOverlap) {
			// If we allow overlap, then we'll want to find a current source which
			// is NOT playing.
			return currentSources.Find((AudioSource obj) => !obj.isPlaying);
		}
		else {
			if(currentSources.Count > 0) {
				return currentSources[0];
			}
			else {
				return null;
			}
		}
	}

	private AudioSource GetNewAudioSource() {
		AudioSource src = audioService.GetAudioSource(
			(AudioSource reclaimedObj) => { currentSources.Remove(reclaimedObj); }
		);

		currentSources.Add(src);

		ConfigureAudioSource(src);

		return src;
	}

	/// <summary>
	/// Do any configuration that is the same every time. Call this when acquiring a
	/// new AudioSource.
	/// </summary>
	/// <param name="target">AudioSource to configure.</param>
	private void ConfigureAudioSource(AudioSource target) {
		target.clip = clip;
		target.outputAudioMixerGroup = mixerGroup;
		target.volume = volume;
	}

	/// <summary>
	/// Randomizes the audio source according to playback options. Call this 
	/// every time the sound is played.
	/// </summary>
	/// <param name="target">Target.</param>
	private void RandomizeAudioSource(AudioSource target) {
		target.pitch = Mathf.Clamp(
			pitch + Random.Range(-pitchRange, pitchRange),
			-3f,
			3f
		);
	}
	#endregion
}
