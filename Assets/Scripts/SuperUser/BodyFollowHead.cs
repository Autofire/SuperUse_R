using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class BodyFollowHead : MonoBehaviour {

		[SerializeField] private TransformConstReference followTarget;

		[SerializeField] private FloatConstReference driftSpeed;

		private void Update() {
			transform.position = Vector3.MoveTowards(
				transform.position,
				followTarget.constValue.position,
				driftSpeed.constValue * Time.deltaTime
			);
		}

	}

}