using UnityEngine;
using UnityEngine.Assertions;

namespace VariableObjects.Base {

	public class Register<Type, VarType> : MonoBehaviour
		where VarType : NullableVariable<Type>
		where Type : class {

		enum ConflictSolution {
			OverwriteOther,
			KeepOther
		};

		enum CleanupTrigger {
			OnDisable,
			OnDestroy
		}

		[SerializeField] VarType variable;
		[SerializeField] Type targetValue;

		[Header("Conflict handling")]
		[SerializeField] ConflictSolution solution;
		[SerializeField] bool deleteSelfIfUnused;
		[SerializeField] CleanupTrigger cleanup;

		private void Awake() {
			Assert.IsNotNull(variable);
			Assert.IsNotNull(targetValue);
		}

		private void OnEnable() {

			// There seems to be a bug in C# that causes the nomral check,
			// (variable.value == null), to ALWAYS return false, even when
			// variable.value really is null. This is despite labeling the
			// Type parameter as being a class (so it can be made null) and
			// using the NullableVariable, which also requires the value to
			// be nullable.
			//
			// However, we can always get the ToString, and it will return
			// null if the value is truly null. This is a bit of a hack,
			// though, and it is rather inefficient.
			if(variable.value.ToString() == "null") {
				variable.value = targetValue;
				Debug.Log("Assinging myself");
			}
			else if(variable.value != targetValue) {
				switch(solution) {
					case ConflictSolution.OverwriteOther:
						variable.value = targetValue;
						break;

					case ConflictSolution.KeepOther:
						if(deleteSelfIfUnused) {
							Destroy(gameObject);
						}
						break;
				}
			}
		}

		private void OnDisable() {
			if(cleanup == CleanupTrigger.OnDisable && variable.value == targetValue) {
				variable.value = null;
			}
		}

		private void OnDestroy() {
			if(cleanup == CleanupTrigger.OnDestroy && variable.value == targetValue) {
				variable.value = null;
			}
		}

	} // End class
} // End namespace