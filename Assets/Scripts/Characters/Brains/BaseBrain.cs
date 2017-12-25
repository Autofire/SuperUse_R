using UnityEngine;

namespace Characters.Brains {

	// This is an adaptation of code from https://bitbucket.org/richardfine/scriptableobjectdemo
	public abstract class BaseBrain : ScriptableObject {

		/// <summary>
		/// Initializes the brain based on the given body.
		/// </summary>
		/// <param name="body">Body which contains the this brain.</param>
		abstract public void Initialize(Bodies.BaseBody body);

		/// <summary>
		/// Run the think cycle. Should be called in the Update loop.
		/// </summary>
		/// <param name="body">Body which contains the this brain.</param>
		virtual public void Think(Bodies.BaseBody body) {}

		/// <summary>
		/// Run the think cycle. Should be called in the FixedUpdate loop.
		/// </summary>
		/// <param name="body">Body which contains the this brain.</param>
		virtual public void FixedThink(Bodies.BaseBody body) {}

	}

} // End namespace
