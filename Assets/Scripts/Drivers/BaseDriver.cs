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


		#region Events

		/// <summary>
		/// Called when driver wants to move in the X direction. Note that this will get called with a value of zero
		/// whenever no movement is desired.
		/// 
		/// This gets called on FixedUpdate.
		/// </summary>
		public event MagnitudeCommand OnMoveX;

		/// <summary>
		/// Called when driver wants to move in the Y direction. Note that this will get called with a value of zero
		/// whenever no movement is desired.
		///
		/// This gets called on FixedUpdate.
		/// </summary>
		public event MagnitudeCommand OnMoveY;

		#endregion


		#region Unity messages

		protected virtual void FixedUpdate() {
			IssueCommand(OnMoveX, 0f);
			IssueCommand(OnMoveY, 0f);
		}

		#endregion


		#region Command helpers

		// TODO see about making a generic, non-class templated function to do this

		/// <summary>
		/// Fires all the given command to all subscribers. This is safe to call even if commandDelegate is null.
		/// </summary>
		/// <returns>
		/// The array of results; for each listener that is called, a true or false is returned, depending on
		/// whether the listener was successful in carrying out the command. If null is passed in, then an empty array
		/// is returned.
		/// </returns>
		/// <param name="commandDelegate">The delegate to fire. This can be null or multicase.</param>
		protected bool[] IssueCommand(Command commandDelegate) {

			// Would use an array, but it's a pain to call 
			List<bool> results = new List<bool>(commandDelegate.GetInvocationList().Length);

			if(commandDelegate != null) {

				foreach (Command cmd in commandDelegate.GetInvocationList()) {
					results.Add(cmd());
				}
			}

			return results.ToArray();
		}

		/// <summary>
		/// Fires all the given command to all subscribers. This is safe to call even if commandDelegate is null.
		/// </summary>
		/// <returns>
		/// The array of results; for each listener that is called, a true or false is returned, depending on
		/// whether the listener was successful in carrying out the command. If null is passed in, then an empty array
		/// is returned.
		/// </returns>
		/// <param name="commandDelegate">The delegate to fire. This can be null or multicase.</param>
		/// <param name="magnitude">The magnitude to pass to the command functions.</param> 
		protected bool[] IssueCommand(MagnitudeCommand commandDelegate, float magnitude) {

			// Would use an array, but it's a pain to call 
			List<bool> results = new List<bool>(commandDelegate.GetInvocationList().Length);

			if(commandDelegate != null) {

				foreach (MagnitudeCommand cmd in commandDelegate.GetInvocationList()) {
					results.Add(cmd(magnitude));
				}
			}

			return results.ToArray();
		}

		#endregion
	}
}
