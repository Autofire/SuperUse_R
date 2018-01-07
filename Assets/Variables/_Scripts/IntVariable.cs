using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/Int")]
	public class IntVariable : Base.Variable<int> {}

	[System.Serializable]
	public class IntReference : Base.Reference<int, IntVariable> {}

	[System.Serializable]
	public class IntConstReference : Base.ConstReference<int, IntVariable> {}

}
