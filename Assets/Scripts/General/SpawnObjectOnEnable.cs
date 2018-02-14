using UnityEngine;
using VariableObjects;

public class SpawnObjectOnEnable : MonoBehaviour {

	[SerializeField] private GameObjectConstReference objPrefab;
	[SerializeField] private TransformConstReference copyPositionOf;
	[SerializeField] private TransformConstReference copyRotationOf;
	[SerializeField] private TransformConstReference targetParent;
	[SerializeField] private BoolConstReference useMyParentIfNullTargetParent;

	void OnEnable() {
		Vector3 targetPos = Vector3.zero;
		Quaternion targetRot = Quaternion.identity;
		Transform parent;

		if(objPrefab.constValue != null) {
			if(copyPositionOf.constValue != null) {
				targetPos = copyPositionOf.constValue.position;
			}

			if(copyRotationOf.constValue != null) {
				targetRot = copyRotationOf.constValue.rotation;
			}

			if(useMyParentIfNullTargetParent.constValue && targetParent.constValue == null) {
				parent = transform.parent;
			}
			else {
				parent = targetParent.constValue;
			}

			Instantiate(objPrefab.constValue, targetPos, targetRot, parent);
		}
		
	}
}
