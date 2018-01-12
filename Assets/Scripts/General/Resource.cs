using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;

/// <summary>
/// A resource of some kind, whether HP, Mana, whatever. 
/// </summary>
[System.Serializable]
public class Resource {

	[SerializeField] private IntReference _max;
	[SerializeField] private IntReference _min;
	[SerializeField] private IntReference _current;

	#region Properties
	public int max {
		get { return _max.constValue; }
	}

	public int min {
		get { return _min.constValue; }
	}

	public int current {
		get { return _current.constValue; }
	}
	#endregion

	#region Public functions
	/// <summary>
	/// Change the current amount of the resource according to the given delta.
	/// </summary>
	/// <param name="delta">
	/// The amount of change; this is ADDED to the current resource count.
	/// To subtract, give a negative number.
	/// </param>
	/// <returns>True if a change was made to the resource count; false otherwise.</returns>
	public bool Change(int delta) {
		int oldVal = _current.value;

		_current.value = Mathf.Clamp(_current.value + delta, _min.constValue, _max.constValue);

		return oldVal != _current.value;
	}

	/// <summary>
	/// Set the current value to the max.
	/// </summary>
	/// <returns>True if a change was made to the resource count; false otherwise.</returns>
	public bool Maximize() {
		int oldVal = _current.value;

		_current.value = _max.constValue;

		return oldVal != _current.value;
	}

	/// <summary>
	/// Set the current value to the min.
	/// </summary>
	/// <returns>True if a change was made to the resource count; false otherwise.</returns>
	public bool Minimize() {
		int oldVal = _current.value;

		_current.value = _min.constValue;

		return oldVal != _current.value;
	}

	/// <summary>
	/// Change the upper and lower bounds of the resource. Note that the resource count will change to stay within
	/// the new bounds.
	/// 
	/// The newMin variable must be below the newMax variable. If this is not the case, either an exception will be
	/// thrown (if in the Editor) or nothing will happen (if in the player).
	/// </summary>
	/// <returns>
	/// <c>true</c>, if resource count was changed as a result of the bounds changing,
	/// <c>false</c> otherwise.
	/// </returns>
	/// <param name="newMin">New minimum resource count.</param>
	/// <param name="newMax">New maximum resource count.</param>
	public bool ChangeBounds(int newMin, int newMax) {
		if(newMax > newMin) {
			_max.value = newMax;
			_min.value = newMin;
			return Change(0);
		}
		else {
			Debug.LogError(
				"The lower bound must be below the upper bound!\n" +
				"(Hint: Check the rest of this message. The stack trace is your best friend right now.)"
			);
			return false;
		}
	}
	#endregion

} // End of class
