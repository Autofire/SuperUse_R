using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/Vector3")]
	public class Vector3Variable : Base.Variable<Vector3> {}

	[System.Serializable]
	public class Vector3Reference : Base.Reference<Vector3, Vector3Variable> {}

	[System.Serializable]
	public class Vector3ConstReference : Base.ConstReference<Vector3, Vector3Variable> {}

}
