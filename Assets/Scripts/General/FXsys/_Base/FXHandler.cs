using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FXHandler : MonoBehaviour {

	[Tooltip("A comment as to what the FX handler is used for. Means nothing in code.")]
	[SerializeField] protected string comment = "";

	/// <summary>
	/// If true, run Play() every time this component is enabled.
	/// </summary>
	[Tooltip("If true, run Play() every time this component is enabled.")]
	[SerializeField] bool playOnEnable = false;

	/// <summary>
	/// If true, run Stop() every time this component is disabled.
	/// </summary>
	[Tooltip("If true, run Stop() every time this component is disabled.")]
	[SerializeField] bool stopOnDisable = false;

	protected virtual void OnEnable() {
		if(playOnEnable) {
			Play();
		}
	}

	protected virtual void OnDisable() {
		if(stopOnDisable) {
			Stop();
		}
	}

	/// <summary>
	/// Play the given effect. This can potentially loop until Stop() is run.
	/// </summary>
	public abstract void Play();

	/// <summary>
	/// Stop playing the given effect. This will not necessarily clean up everything in the effect
	/// immediately, but it should eventually come to a stop.
	/// </summary>
	public abstract void Stop();

	/// <summary>
	/// Determines whether this effect is currently playing. Should return true/false immediately after
	/// Play()/Stop() is called, respectively.
	/// </summary>
	/// <returns><c>true</c> if this instance is playing; otherwise, <c>false</c>.</returns>
	public abstract bool IsPlaying();

	/// <summary>
	/// An alias for Play() and Stop().
	/// </summary>
	/// <param name="startPlaying">If set to <c>true</c>, call Play(). Otherwise, call Stop().</param>
	public void Play(bool startPlaying) {
		if(startPlaying)
			Play();
		else
			Stop();
	}

	/// <summary>
	/// If the targetFX is null, then a NullFX is created, attached to targetObject, and returned.
	/// Use this to avoid NullReferenceExceptions.
	/// </summary>
	/// <param name="targetFX">FX to check if null.</param>
	/// <param name="targetObject">Object to place the NullFX on.</param>
	public static FXHandler Check(FXHandler targetFX, GameObject targetObject) {
		if(targetFX == null) {
			#if UNITY_EDITOR
			Debug.Log("Creating NullFX on " + targetObject.name);
			#endif

			return targetObject.AddComponent<NullFX>() as FXHandler;
		}
		else {
			return targetFX;
		}
	}
}
