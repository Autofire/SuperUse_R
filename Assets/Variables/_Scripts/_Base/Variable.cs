namespace VariableObjects.Base {

	/// <summary>
	/// A variable container object.
	/// </summary
	/// <typeparam name="T">Core variable type.</typeparam>
	public class Variable<T> : UnityEngine.ScriptableObject {
		public T value;
	}
}
