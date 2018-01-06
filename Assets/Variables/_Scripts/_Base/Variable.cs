namespace VariableObjects.Base {

	/// <summary>
	/// A variable container object.
	/// </summary
	/// <typeparam name="T">Core variable type.</typeparam>
	public class Variable<T> : EventObjects.GameEvent { 
		public T _value;

		public T value {
			set {
				_value = value;
				Raise();
			}
			get { return _value; }
		}
	}
}
