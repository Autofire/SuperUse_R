using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Brains {

	/// <summary>
	/// This is the basic driver, which determines how characters behave. It can either be with input from a user or
	/// based on AI.
	///
	/// Note that not all actions 
	/// </summary>
	public abstract class BaseBrain : MonoBehaviour {


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

			if(commandDelegate != null) {
				List<bool> results = new List<bool>(commandDelegate.GetInvocationList().Length);

				results = new List<bool>(commandDelegate.GetInvocationList().Length);

				foreach (Command cmd in commandDelegate.GetInvocationList()) {
					results.Add(cmd());
				}

				return results.ToArray();
			}
			else {
				return new bool[0];
			}
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


			if(commandDelegate != null) {
				List<bool> results = new List<bool>(commandDelegate.GetInvocationList().Length);

				results = new List<bool>(commandDelegate.GetInvocationList().Length);

				foreach (MagnitudeCommand cmd in commandDelegate.GetInvocationList()) {
					results.Add(cmd(magnitude));
				}

				return results.ToArray();
			}
			else {
				return new bool[0];
			}

		}

		#endregion
	}
}
