using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableObjects.Base {

	[System.Serializable]
	public class Reference<Type, VarType> : ConstReference<Type, VarType>
		where VarType: Variable<Type> {

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