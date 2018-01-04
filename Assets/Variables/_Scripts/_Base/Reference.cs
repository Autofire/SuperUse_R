using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableObjects.Base {

	/// <summary>
	/// A reference to a variable. It can be both accessed and modified.
	/// </summary>
	/// <typeparam name="Type">Core variable type.</typeparam>
	/// <typeparam name="VarType">Variable type; must match up with 'Type'.</typeparam>
	[System.Serializable]
	public class Reference<Type, VarType> : ConstReference<Type, VarType>
		where VarType: Variable<Type> {

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public Type value {
			get {
				return constValue;
			}
			set {
				if(useInternal) {
					internalValue = value;
				}
				else {
					variable.value = value;
				}
			}
		} // End value
	
	} // End Reference

} // End namespace