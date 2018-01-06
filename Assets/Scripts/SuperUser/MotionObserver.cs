using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;

namespace SuperUser {

	/// <summary>
	/// Observes the VR player's gestures/motions and
	/// lets listeners know.
	/// </summary>
	public class MotionObserver : MonoBehaviour {

		[SerializeField] TransformConstReference leftUpper;
		[SerializeField] TransformConstReference leftLower;

		[SerializeField] TransformConstReference rightUpper;
		[SerializeField] TransformConstReference rightLower;

		[Space(5)] [Header("Clasp")]
		[SerializeField] FloatConstReference lowerToLowerMinDist;

		private void Awake() {
			Assert.IsNotNull(leftUpper);
			Assert.IsNotNull(leftLower);

			Assert.IsNotNull(rightUpper);
			Assert.IsNotNull(rightLower);
		}

		private void Update() {
			// Controllers "clasp"

			if(leftLower.constValue != null && rightLower.constValue != null) {
				Debug.Log(
					"L-Lower to R-Lower: " + (leftLower.constValue.position - rightLower.constValue.position).magnitude.ToString()
					+ "\n" +
					"L-Lower to R-Upper: " + (leftLower.constValue.position - rightUpper.constValue.position).magnitude.ToString()
					+ "\n" +
					"L-Upper to R-Lower: " + (leftUpper.constValue.position - rightLower.constValue.position).magnitude.ToString()
					+ "\n" +
					"L-Upper to R-Upper: " + (leftUpper.constValue.position - rightUpper.constValue.position).magnitude.ToString()
				);
			}
			else {
				Debug.Log("Waiting on both controllers...");
			}
		}
	}
}
