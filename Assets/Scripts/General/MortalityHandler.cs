using UnityEngine;
using EventObjects;

/// <summary>
/// This is a simple component which causes objects to have a "die" function. 
/// </summary>
public class MortalityHandler : MonoBehaviour {

	#region Static methods

	/// <summary>
	/// Kill the specified object, using its MortalityHandler if it has one. If it doesn't have one, the object gets
	/// destroyed on the spot. Note that, under some circumstances, the object in question might be immortal, so then
	/// it won't die.
	/// </summary>
	/// <param name="other">Thing that needs to die.</param>
	public static void Kill(GameObject other) {
		MortalityHandler otherHandler = other.GetComponentInChildren<MortalityHandler>();

		if(otherHandler != null) {
			otherHandler.Die();
		}
		else {
			// When you're an expendable crew-member, you should be able to die even if you don't have any way of dying
			// that's on-script.
			Destroy(other);
		}
	}

	#endregion

	#region Types

	private enum DeathReactionType {
		Nothing, DisableObject, DestroyObject
	}

	#endregion


	#region Variables

	[SerializeField] private bool _isImmortal;	// TODO Make there be a "track isDying while immortal" bool so that we might die the moment we become mortal again
	[SerializeField] private DeathReactionType deathReaction;

	[SerializeField] private GameEventInvoker onDie;
	[SerializeField] private GameEventInvoker onCancelDie;

	#endregion


	#region Properties

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="MortalityHandler"/> is immortal. Immortal objects cannot
	/// be destroyed through their Die() command or by calling Kill(GameObject) on them.
	/// </summary>
	/// <value><c>true</c> if is immortal; otherwise, <c>false</c>.</value>
	public bool isImmortal {
		get {
			return _isImmortal;
		}
		set {
			if(_isImmortal != value) {
				_isImmortal = value;

				if(_isImmortal == true) {
					CancelDie();
				}
			}
		}
	}

	/// <summary>
	/// Tracks if we're dying. This is important because we don't want to accidentally trigger many death sequences.
	/// However, due to using this flag, it is VERY IMPORTANT to call CancelDie() if something interrupts the
	/// death sequence.
	/// </summary>
	public bool isDying {
		get;
		private set;
	}

	#endregion


	#region Functions

	public void Die() {

		if(!isDying && !isImmortal) {
			onDie.Invoke();

			isDying = true;

			switch(deathReaction) {
			case DeathReactionType.DestroyObject:
				Destroy(gameObject);
				break;
			case DeathReactionType.DisableObject:
				gameObject.SetActive(false);
				break;
			}
		}
	}

	public void CancelDie() {

		if(isDying) {
			onCancelDie.Invoke();

			isDying = false;
		}
	}

	#endregion
}
