using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VariableObjects.Base {

	[System.Serializable]
	public class ConstReference<Type, VarType>
		where VarType: Variable<Type> {

		[SerializeField] protected bool _useInternal = true;
		[SerializeField] protected Type internalValue;
		[SerializeField] protected VarType variable;

		public bool useInternal {
			get { return _useInternal; }
		}

		public Type constValue {
			get {
				return useInternal ? internalValue : variable.value;
			}
		} // End value

	} // End ConstReference

} // End namespace