using UnityEngine;
using VariableObjects;

namespace SuperUser {

	public class BodyFollowHead : MonoBehaviour {

		[SerializeField] private TransformConstReference followTarget;

		[SerializeField] private FloatConstReference driftSpeed;
		[SerializeField] private FloatConstReference rotationSpeed;

		private void Update() {
			transform.position = Vector3.MoveTowards(
				transform.position,
				followTarget.constValue.position,
				driftSpeed.constValue * Time.deltaTime
			);

			Vector3 targetForward = Vector3.ProjectOnPlane(followTarget.constValue.forward, Vector3.up);
			Quaternion offsetAngle = Quaternion.FromToRotation(transform.forward, targetForward);
			Quaternion currentRot = transform.localRotation;

			transform.localRotation = Quaternion.RotateTowards(
				currentRot,
				offsetAngle * currentRot,
				rotationSpeed.constValue * Time.deltaTime
			);
		}

	}

}