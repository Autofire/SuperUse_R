using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Brains;

namespace Characters.Bodies {

	// This is an adaptation of code from https://bitbucket.org/richardfine/scriptableobjectdemo
	public class BaseBody : MonoBehaviour {

		#region Brain
		[SerializeField] BaseBrain _brain;

		/// <summary>
		/// The brain which is currently being used by the body.
		/// </summary>
		/// <value>The brain.</value>
		public BaseBrain brain {
			get { return _brain; }
			set {
				if(value != _brain) {
					_brain = value;

					if(enabled) {
						PrepNewBrain();
					}
				}
			}
		}
		#endregion
			
		#region Memory
		/// <summary>
		/// The memory across all brains that have ever been contained in this object. See the following functions in
		/// this region for how to manipulate the memories.
		///
		/// In general, the memory for one particular brain is the "specific memory."
		/// </summary>
		private Dictionary<System.Type, Dictionary<string, object>> memory;

		/// <summary>
		/// Initializes the specific memory for the current brain. If there was any memories stored it in before, they
		/// are erased.
		///
		/// This must get called before any Remember functions may be called for the currently stored brain.
		/// </summary>
		public void InitMemory() {
			memory[brain.GetType()] = new Dictionary<string, object>();
		}

		/// <summary>
		/// Retrieves the value stored in memory that is associated with the current brain and the given key.
		/// </summary>
		/// <param name="key">Name of the memory.</param>
		/// <typeparam name="T">Type of object stored in the memory.</typeparam>
		public T Remember<T>(string key)
		{
			Dictionary<string, object> specificMemory;
			object result;

			// Yes, this works; short circuiting is nice.
			if(memory.TryGetValue(brain.GetType(), out specificMemory) && specificMemory.TryGetValue(key, out result)) {
				return (T)result;
			}
			else {
				return default(T);
			}
		}

		/// <summary>
		/// Remember the value with the given key. It's stored in the current brain's specific memory.
		/// </summary>
		/// <param name="key">Name of the memory.</param>
		/// <param name="value">Object to store as a memory.</param>
		/// <typeparam name="T">Type of object stored in the memory.</typeparam>
		public void Remember<T>(string key, T value)
		{
			memory[brain.GetType()][key] = value;
		}
		#endregion

		#region Unity events
		virtual protected void Awake() {
			memory = new Dictionary<System.Type, Dictionary<string, object>>();
		}

		virtual protected void OnEnable()
		{
			PrepNewBrain();
		}

		virtual protected void Update () {
			brain.Think(this);
		}

		virtual protected void FixedUpdate() {
			brain.FixedThink(this);
		}
		#endregion

		private void PrepNewBrain() {
			if (!brain) {
				enabled = false;
			}
			else {
				System.Type brainType = brain.GetType();

				if(!memory.ContainsKey(brainType)) {
					InitMemory();
				}

				brain.Initialize(this);
			}
		}
		/*
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
		}*/

		//protected abstract void AttachBrain(BaseBrain target);
		//protected abstract void DetachBrain(BaseBrain target);
	}

}