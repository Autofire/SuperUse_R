using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drivers {

	/// <summary>
	/// This is the basic driver, which determines how characters behave. It can either be with input from a user or
	/// based on AI.
	///
	/// Note that not all actions 
	/// </summary>
	public abstract class BaseDriver : MonoBehaviour {

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


		/// <summary>
		/// Called when driver wants to move in the X direction. Note that this will get called with a value of zero
		/// whenever no movement is desired.
		/// </summary>
		public event MagnitudeCommand OnMoveX;

		/// <summary>
		/// Called when driver wants to move in the Y direction. Note that this will get called with a value of zero
		/// whenever no movement is desired. 
		/// </summary>
		public event MagnitudeCommand OnMoveY;

		public event Command OnJump;


			
	}
}
