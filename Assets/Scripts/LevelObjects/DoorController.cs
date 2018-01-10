using UnityEngine;
using VariableObjects;

public class DoorController : MonoBehaviour {

	#region Serialized fields

	[Tooltip("Time door takes to fully open")]
	[SerializeField] FloatConstReference openTime;
	[Tooltip("Time door takes to fully close")]
	[SerializeField] FloatConstReference closeTime;
	[Tooltip("Offset which the door moves to when fully open")]
	[SerializeField] Vector3 openOffset;
	[SerializeField] BoolReference isOpen;

	#endregion


	#region Public properties

	/// <summary>
	/// If true, the door has come to a complete stop.
	/// </summary>
	public bool finishedMoving {
		get {
			return targetPos == transform.localPosition;
		}
	}

	#endregion

	#region Private variables and properties
	private Vector3 closePos;
	private Vector3 openPos {
		get { return closePos + openOffset; }
	}
	private float distance {
		get { return Vector3.Distance(openPos, closePos); }
	}

	private Vector3 targetPos {
		get {
			if(isOpen.constValue) {
				return openPos;
			}
			else {
				return closePos;
			}
		}
	}

	private float velocity {
		get {
			if(isOpen.constValue) {
				return distance / openTime.constValue;
			}
			else {
				return distance / closeTime.constValue;
			}
		}
	}
	#endregion

	#region Unity events
	private void Awake() {
		closePos = transform.localPosition;
	}

	private void Update () {
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, velocity * Time.deltaTime );
	}
	#endregion

	#region Public functions

	public void ToggleOpen() {
		isOpen.value = !isOpen.value;
	}

	public void SetOpen(bool newStatus) {
		isOpen.value = newStatus;
	}

	#endregion
}
