using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/Float")]
	public class FloatVariable : Base.Variable<float> {}

	[System.Serializable]
	public class FloatReference : Base.Reference<float, FloatVariable> {}

	[System.Serializable]
	public class FloatConstReference : Base.ConstReference<float, FloatVariable> {}

}