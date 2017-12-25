using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Bodies {

	public interface IStand {
		/// <summary>
		/// If we're standing, then this returns true. Otherwise, we're airborne or something.
		/// </summary>
		/// <returns><c>true</c> if the body is standing; otherwise, <c>false</c>.</returns>
		bool IsStanding();
	}

	public interface IJump {
		/// <summary>
		/// Initiate a jump.
		/// </summary>
		void JumpBegin();

		/// <summary>
		/// Ends a jump, usually prematurely. This might get called long after the jump is finished, or maybe never at
		/// all. Because of this, don't depend on this getting called.
		/// </summary>
		void JumpEnd();
	}

	public interface IMoveX {
		/// <summary>
		/// Moves the body in the X direction.
		/// </summary>
		/// <param name="magnitude">Magnitude of the movement; can be between -1 and 1, inclusive.</param>
		void MoveX(float magnitude);
	}

	public interface IMoveY {
		/// <summary>
		/// Moves the body in the Y direction.
		/// </summary>
		/// <param name="magnitude">Magnitude of the movement; can be between -1 and 1, inclusive.</param>
		void MoveY(float magnitude);
	}

} // End namespace
