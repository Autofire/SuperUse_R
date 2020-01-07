using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;
using EventObjects;

public class PickupController : MonoBehaviour {

	[SerializeField] private IntVariable countVariable;
	[SerializeField] private GameEventInvoker lastOneEvent;

	private void Awake() {
		Assert.IsNotNull(countVariable);
	}

	private void Start() {
		countVariable.value++;
	}

	private void OnTriggerEnter(Collider other) {
		MortalityHandler.Kill(gameObject);

		countVariable.value--;

		if(countVariable.value == 0) {
			lastOneEvent.Invoke();
		}
	}
}
