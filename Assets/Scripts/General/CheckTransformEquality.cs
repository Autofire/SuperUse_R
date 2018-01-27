using UnityEngine;
using UnityEngine.Events;
using VariableObjects;

public class CheckTransformEquality : MonoBehaviour {

	[SerializeField] private TransformConstReference target;
	[SerializeField] private UnityEvent ifEqual;
	[SerializeField] private UnityEvent ifNotEqual;

	public void Check() {
		if(target.constValue == transform) {
			ifEqual.Invoke();
		}
		else {
			ifNotEqual.Invoke();
		}
	}
}
