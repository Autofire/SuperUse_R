using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/Bool")]
	public class BoolVariable : Base.Variable<bool> {}

	[System.Serializable]
	public class BoolReference : Base.Reference<bool, BoolVariable> {}

	[System.Serializable]
	public class BoolConstReference : Base.ConstReference<bool, BoolVariable> {}

}
