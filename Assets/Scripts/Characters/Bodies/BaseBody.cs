using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Brains;

namespace Characters.Bodies {

	public class BaseBody : MonoBehaviour {

		// TODO make this into an array
		[SerializeField] BaseBrain brains;


		protected virtual void OnEnable() {
			if(brains is IOnJumpBegin) {
				((IOnJumpBegin)brains).OnJumpBegin += testMeepo;
			}

			if(brains is IOnJumpEnd) {
				((IOnJumpEnd)brains).OnJumpEnd += testMeepo2;
			}
		}

		protected virtual void OnDisable() {
			if(brains is IOnJumpBegin) {
				((IOnJumpBegin)brains).OnJumpBegin -= testMeepo;
			}

			if(brains is IOnJumpEnd) {
				((IOnJumpEnd)brains).OnJumpEnd -= testMeepo2;
			}
		}

		bool testMeepo ()
		{
			Debug.Log("I'm a test Meepo who is jumping!");
			return true;
		}

		bool testMeepo2 ()
		{
			Debug.Log("I'm a test Meepo who is not jumping!");
			return true;
		}

		//protected abstract void AttachBrain(BaseBrain target);
		//protected abstract void DetachBrain(BaseBrain target);
	}

}