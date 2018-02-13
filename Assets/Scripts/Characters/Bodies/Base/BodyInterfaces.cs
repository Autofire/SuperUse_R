using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Bodies {

	#region Basic interfaces 

	public interface IStand {
		/// <summary>
		/// If we're standing, then this returns true. Otherwise, we're airborne or something.
		/// </summary>
		/// <returns><c>true</c> if the body is standing; otherwise, <c>false</c>.</returns>
		bool IsStanding();
	}

	public interface IBumpHead {
		/// <summary>
		/// Determines whether this instance is bumping head. (This is the default description, and it's glorious.)
		/// </summary>
		/// <returns><c>true</c> if this instance is bumping head; otherwise, <c>false</c>.</returns>
		bool IsBumpingHead();
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

		float jumpMaxHeight { get; }
		float jumpPeekDist  { get; }
		float jumpMaxDist   { get; }
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

	public interface IMoveOnPathTowards {
		/// <summary>
		/// Move towards some point, using this body's own methods of getting there.
		/// </summary>
		/// <returns>True if we predict that we can reach the location; false otherwise.</returns>
		/// <param name="target">Target position.</param>
		bool MoveOnPathTowards(Vector3 target);

		/// <summary>
		/// Move towards some point, using this body's own methods of getting there.
		/// </summary>
		/// <returns>True if we predict that we can reach the location; false otherwise.</returns>
		/// <param name="target">Target transform.</param>
		bool MoveOnPathTowards(Transform target);
	}

	public interface IHaveDirections {
		Vector3 forward { get; }
		Vector3 right   { get; }
		Vector3 up      { get; }
	}

	#endregion

	#region Complex interfaces

	public interface IPlatformer : IStand, IMoveX, IJump
	{ }

	#endregion

} // End namespace
