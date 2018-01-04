using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableObjects.Base {

	/// <summary>
	/// A constant reference to variable. It cannot be changed.
	/// </summary>
	/// <typeparam name="Type">Core variable type.</typeparam>
	/// <typeparam name="VarType">Variable type; must match up with 'Type'.</typeparam>
	[System.Serializable]
	public class ConstReference<Type, VarType>
		where VarType: Variable<Type> {

		[SerializeField] protected bool _useInternal = true;
		[SerializeField] protected Type internalValue;
		[SerializeField] protected VarType variable;

		// In theory, we could create a copy of the object and then
		// return that, but this can get really piggy (or, in some cases,
		// impossible) when it comes to objects.

		/// <summary>
		/// Gets the readonly value; you cannot re-assign into this.
		/// In the case of complex datatypes (like classes), please do not
		/// lie and make changes to the object. If you need to do that, please
		/// look at the Reference object that goes with your type.
		/// </summary>
		/// <value>The const value.</value>
		public Type constValue {
			get {
				return _useInternal ? internalValue : variable.value;
			}
		} // End value

	} // End ConstReference

} // End namespace