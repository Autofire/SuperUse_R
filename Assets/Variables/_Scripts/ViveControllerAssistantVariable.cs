using UnityEngine;
using SuperUser;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/ViveControllerAssistant")]
	public class ViveControllerAssistantVariable : Base.NullableVariable<ViveControllerAssistant> {}

	[System.Serializable]
	public class ViveControllerAssistantReference : Base.Reference<ViveControllerAssistant, ViveControllerAssistantVariable> {}

	[System.Serializable]
	public class ViveControllerAssistantConstReference : Base.ConstReference<ViveControllerAssistant, ViveControllerAssistantVariable> {}

}
