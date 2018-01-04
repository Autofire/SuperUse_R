using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/Transform")]
	public class TransformVariable : Base.NullableVariable<Transform> {}

	[System.Serializable]
	public class TransformReference : Base.Reference<Transform, TransformVariable> {}

	[System.Serializable]
	public class TransformConstReference : Base.ConstReference<Transform, TransformVariable> {}

}
