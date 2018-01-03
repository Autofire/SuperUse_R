using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName="Variable/")]
namespace VariableObjects.Base {

	public class Variable<T> : ScriptableObject {
		public T value;
	}
}
