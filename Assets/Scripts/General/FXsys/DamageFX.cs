using UnityEngine;
using System.Collections;

using UniversalColors;

/// <summary>
/// Deals with playing the damage effects on the object it's attached to. Just because this effect is on
/// the object does not necessarily mean that damage effects will play; they only will if it it has either targetObj
/// or targetCC.
/// </summary>
public class DamageFX : ColorableComponentFXHandler {

	[SerializeField] Color hurtFlashColor = Color.red;
	[SerializeField] float hurtFlashTime  = 0.2f;

	private IEnumerator effectCoroutine = null;

	public override void Play() {
		Play(1);
	}

	public void Play(int dmgAmt) {
		base.Play();

		CleanUp();

		effectCoroutine = GetHitEffect(dmgAmt);
		StartCoroutine(effectCoroutine);
	}

	public override void Stop() {
		base.Stop();

		CleanUp();
	}
	
	/// <summary>
	/// This is faulty! Returns true when another script is acting upon the color!
	/// </summary>
	/// <returns></returns>
	public override bool IsPlaying() {
		return effectCoroutine != null || base.IsPlaying();
	}


	private IEnumerator GetHitEffect(int damageAmount = 1) {
		if(targetCC != null)
		{
			float startTime;

			startTime = Time.time;

			while(Time.time < startTime + hurtFlashTime)
			{
				targetCC.color =
					Color.Lerp(
						baseColor, 
						hurtFlashColor, 
						Mathf.Cos((Time.time - startTime) * Mathf.PI / 2 / hurtFlashTime)
					);
				yield return null;
			}

		} // End if(targetRenderer != null)

		CleanUp();
	}

	private void CleanUp() {
		if(effectCoroutine != null) {
			StopCoroutine(effectCoroutine);
			effectCoroutine = null;
		}

		// We've recieved some null-reference exceptions related to targetCC being non-existant, i.e. when something dies.
		if(targetCC != null) {
			targetCC.color = baseColor;
		}
	}
}
