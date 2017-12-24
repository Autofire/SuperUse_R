using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Brains {
	
	#region Types

	/// <summary>
	/// Events of this type let other objects know when a command is given.
	///
	/// Returns true when the action is successful (i.e. nothing stopped it from being carried out).
	/// </summary>
	public delegate bool Command();

	/// <summary>
	/// Events of this type let other objects know when a command is given. These commands have some magnitude
	/// assitiated with them.
	///
	/// Returns true when the action is successful (i.e. nothing stopped it from being carried out).
	/// </summary>
	public delegate bool MagnitudeCommand(float magnitude);

	#endregion



	public interface IOnMoveX {
		/// <summary>
		/// Called when driver wants to move in the X direction. Note that this will get called with a value of zero
		/// whenever no movement is desired.
		/// 
		/// This gets called on FixedUpdate.
		/// </summary>
		event MagnitudeCommand OnMoveX; 
	}

	public interface IOnMoveY {
		/// <summary>
		/// Called when driver wants to move in the Y direction. Note that this will get called with a value of zero
		/// whenever no movement is desired.
		///
		/// This gets called on FixedUpdate.
		/// </summary>
		event MagnitudeCommand OnMoveY;
	}

	public interface IOnJumpBegin {
		/// <summary>
		/// Called when the driver wants to jump. Note that this is not the same thing has having a positive Y
		/// direction.
		///
		/// This gets called on Update; using FixedUpdate may yield ghost inputs.
		/// </summary>
		event Command OnJumpBegin;
	}

	public interface IOnJumpEnd {
		/// <summary>
		/// Called when the driver has hit the peak of their desired jump. This may never get called.
		///
		/// This gets called on Update; using FixedUpdate may yield ghost inputs.
		/// </summary>
		event Command OnJumpEnd;
	}

}