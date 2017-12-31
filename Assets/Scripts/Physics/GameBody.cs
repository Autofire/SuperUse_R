using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhysics {

	abstract public class GameBody : MonoBehaviour {
		/// <summary>
		/// Move with the specified motion. In otherwords, attempt to apply the given
		/// offset to the object. This will only apply the motion as far as is available,
		/// and stops short if there is something in the way.
		/// 
		/// The object is garunteed to only move along the direction of the motion.
		/// </summary>
		/// <param name="motion">
		/// The actual motion. If there was nothing in the way,
		/// this the return value is equal to the motion which was passed in.
		/// </param>
		abstract public Vector3 Move(Vector3 motion);

		/// <summary>
		/// Project our object in the given direction and distance.
		/// This finds the first possible hit, and it does not return
		/// anything in the case of overlapping colliders.
		/// </summary>
		///
		/// <param name="direction">
		/// Direction to fire the cast in.
		/// This directoin is relative to the object's current facing.
		/// </param>
		///
		/// <param name="distance">
		/// Distance we want to project out to. THis is the distance
		/// which we expect the object's center to travel as a whole.
		/// </param>
		///
		/// <returns>
		/// The hit info if something was hit; null otherwise.
		/// </returns>
		abstract public System.Nullable<RaycastHit> Cast(Vector3 direction, float distance);

		public System.Nullable<RaycastHit> Cast(Vector3 motion) {
			return Cast(motion, motion.magnitude);
		}


		/// <summary>
		/// Project our object in the given direction and distance.
		/// This returns the info for all possible hits.
		/// </summary>
		///
		/// <param name="direction">
		/// Direction to fire the cast in.
		/// This directoin is relative to the object's current facing.
		/// </param>
		///
		/// <param name="distance">
		/// Distance we want to project out to. THis is the distance
		/// which we expect the object's center to travel as a whole.
		/// </param>
		///
		/// <returns>
		/// The info on all potential collisions. If no collisions are
		/// found, then an empty list is returned. Be wary that, for a
		/// given hit, if it has a distance of zero and a point of zero,
		/// then we have an overlap in our collision.
		/// </returns>
		abstract public RaycastHit[] CastAll(Vector3 direction, float distance);

		public RaycastHit[] CastAll(Vector3 motion) {
			return CastAll(motion, motion.magnitude);
		}

	} // End of namespace
} // End of namespace