using UnityEngine;
using UnityEngine.Assertions;

namespace VariableObjects.Base {

	public class Register<Type, VarType> : MonoBehaviour
		where VarType : NullableVariable<Type>
		where Type : class {

		enum ConflictSolution {
			OverwriteOther,
			KeepOther,
			KeepOtherAndDeleteSelf
		};

		enum CleanupTrigger {
			OnDisable,
			OnDestroy
		}

		[SerializeField] VarType variable;
		[SerializeField] Type targetValue;

		[Space(10)]
		[SerializeField] ConflictSolution uponConflict;
		[SerializeField] CleanupTrigger cleanupSignal;

		private void Awake() {
			Assert.IsNotNull(variable);
			Assert.IsNotNull(targetValue);
		}

		private void OnEnable() {

			// There seems to be a bug in C# that causes the normal check,
			// (variable.value == null), to ALWAYS return false, even when
			// variable.value really is null. This is despite labeling the
			// Type parameter as being a class (so it can be made null) and
			// using the NullableVariable, which also requires the value to
			// be nullable.
			//
			// However, we can always get the ToString, and it will return
			// null if the value is truly null. This is a bit of a hack,
			// though, and it is rather inefficient.
			//
			// That being said, there are times when variable.value *does*
			// turn up as being null, so it's important to check anyway.
			if(variable.value == null || variable.value.ToString() == "null") {
				variable.value = targetValue;
			}
			else if(variable.value != targetValue) {
				switch(uponConflict) {
					case ConflictSolution.OverwriteOther:
						variable.value = targetValue;
						break;

					case ConflictSolution.KeepOtherAndDeleteSelf:
						Destroy(gameObject);
						break;

					case ConflictSolution.KeepOther:
						break;
				}
			}
		}

		private void OnDisable() {
			if(cleanupSignal == CleanupTrigger.OnDisable && variable.value == targetValue) {
				variable.value = null;
			}
		}

		private void OnDestroy() {
			if(cleanupSignal == CleanupTrigger.OnDestroy && variable.value == targetValue) {
				variable.value = null;
			}
		}

	} // End class
} // End namespace