using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VariableObjects.Base {

	/// <summary>
	/// A variable container object.
	/// </summary
	/// <typeparam name="T">Core variable type.</typeparam>
	public class Variable<T> : ScriptableObject {
		public T value;
	}
}
