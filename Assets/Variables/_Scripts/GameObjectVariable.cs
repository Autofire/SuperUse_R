using UnityEngine;

namespace VariableObjects {

	[CreateAssetMenu(menuName="Variable/GameObject")]
	public class GameObjectVariable : Base.NullableVariable<GameObject> {}

	[System.Serializable]
	public class GameObjectReference : Base.Reference<GameObject, GameObjectVariable> {}

	[System.Serializable]
	public class GameObjectConstReference : Base.ConstReference<GameObject, GameObjectVariable> {}

}
