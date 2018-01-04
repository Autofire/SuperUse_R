namespace VariableObjects.Base {

	/// <summary>
	/// A variable container object which may contain a null value.
	/// </summary
	/// <typeparam name="T">Core variable type.</typeparam>
	public class NullableVariable<T> : Variable<T>
		where T : class { }
}