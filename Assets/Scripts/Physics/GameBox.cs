using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhysics {

	public class GameBox : GameBody {

		[SerializeField] LayerMask collisionMask = -1;
		[SerializeField] BoxCollider boundingBox;
		[SerializeField] protected float skinThickness = 0.05f;
		[Space(10)]
		[SerializeField] bool debugCasts = false;
		[SerializeField] float debugCastLifetime = 0f;


		override public Vector3 Move(Vector3 motion)
		{
			Vector3 finalMotion = motion;

			Vector3 halfBoxSize = boundingBox.size * 0.5f;
			Vector3 castBoxSize = halfBoxSize - Vector3.one * skinThickness;

			float maxTravelDist    = motion.magnitude + skinThickness;
			float targetTravelDist = maxTravelDist;

			if(debugCasts) {
				// Draw the initial cast.
				ExtDebug.DrawBoxCastBox(
					origin:      transform.position,
					halfExtents: castBoxSize,
					orientation: transform.rotation,
					direction:   motion,
					distance:    maxTravelDist,
					color:       Color.red,
					duration:    debugCastLifetime
				);
			}

			RaycastHit[] allHitInfo =
				Physics.BoxCastAll(
					center:                  transform.position,
					halfExtents:             castBoxSize,
					direction:               motion,
					orientation:             transform.rotation,
					maxDistance:             maxTravelDist,
					layermask:               collisionMask,
					queryTriggerInteraction: QueryTriggerInteraction.Ignore
				);

			foreach(RaycastHit hitInfo in allHitInfo) {
				// We are going to intersect with the object if we continue down the current path.

				if(debugCasts) {
					// Draw the cast for the collision.
					ExtDebug.DrawBoxCastOnHit(
						origin:          transform.position,
						halfExtents:     castBoxSize,
						orientation:     transform.rotation,
						direction:       motion,
						hitInfoDistance: hitInfo.distance,
						color:           Color.cyan,
						duration:        debugCastLifetime
					);
				}

				if(hitInfo.distance == 0 && hitInfo.point == Vector3.zero) {
					Debug.LogError(
						transform.gameObject.name + " is stuck inside an object named " + hitInfo.collider.gameObject.name + '\n'
						+ "No movement will be made on the object until it's freed."
					);

					targetTravelDist = 0;
				}
				else if(hitInfo.distance - skinThickness < targetTravelDist) {
					targetTravelDist = hitInfo.distance - skinThickness;
				}
			}

			if(targetTravelDist != maxTravelDist) {
				finalMotion = motion.normalized * targetTravelDist;
			}

			transform.position += finalMotion;

			return finalMotion;
		}
	}

}