using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/LayerMask")]
	public class LayerMaskVariable : Base.Variable<LayerMask> {}

	[System.Serializable]
	public class LayerMaskReference : Base.Reference<LayerMask, LayerMaskVariable> {}

	[System.Serializable]
	public class LayerMaskConstReference : Base.ConstReference<LayerMask, LayerMaskVariable> {}

}
