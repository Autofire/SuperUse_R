using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhysics {

	public class GameRigidBox : GamePhysics.GameRigidBody {

		[SerializeField] LayerMask collisionMask = -1;
		[SerializeField] BoxCollider boundingBox;
		[SerializeField] protected float skinThickness = 0.05f;
		[Space(10)]
		[SerializeField] bool debugCasts         = false;
		[Range(0, 10)]
		[SerializeField] float debugCastLifetime = 0.1f;
		[SerializeField] Color debugCastAttempt  = Color.yellow;
		[SerializeField] Color debugCastMiss     = Color.red;
		[SerializeField] Color debugCastHit      = Color.cyan;

		public Vector3 halfBoxSize {
			get { return boundingBox.size * 0.5f; }
		}

		public Vector3 castBoxSize {
			get { return halfBoxSize - Vector3.one * skinThickness; }
		}


		// TODO Make this take a direction and a magnitude, instead of a motion.
		// With this, we can instead return the new magnitude, which is exactly what
		// we want when we're doing our weird shenanghans.
		override public Vector3 Move(Vector3 motion)
		{
			Vector3 initMotion = RelativeToAbsoluteRotation() * motion;
			Vector3 finalMotion = initMotion;

			float maxTravelDist    = initMotion.magnitude + skinThickness;
			float targetTravelDist = maxTravelDist;

			RaycastHit[] allHitInfo = CastAll(initMotion, maxTravelDist);

			foreach(RaycastHit hitInfo in allHitInfo) {

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
				finalMotion = initMotion.normalized * targetTravelDist;
			}

			transform.position += finalMotion;

			return AbsoluteToRelativeRotation() * finalMotion;
		} // End Move



		override public System.Nullable<RaycastHit> Cast(Vector3 direction, float distance) {

			bool hit;
			RaycastHit hitInfo;

			hit = Physics.BoxCast(
				hitInfo:      out hitInfo,

				center:       transform.position,   halfExtents:             castBoxSize,
				direction:    direction,            layerMask:               collisionMask,
				orientation:  transform.rotation,   queryTriggerInteraction: QueryTriggerInteraction.Ignore,
				maxDistance:  distance
			);


			#if UNITY_EDITOR
			if(debugCasts) {
				ExtDebug.DrawBoxCastBox(
					origin:      transform.position, halfExtents: castBoxSize,
					orientation: transform.rotation, direction:   direction,
					distance:    distance,           

					duration:    debugCastLifetime,
					color:       (hit ? debugCastAttempt : debugCastMiss)
				);

				if(hit) {
					ExtDebug.DrawBoxCastOnHit(
						origin:          transform.position,      halfExtents:     castBoxSize,
						orientation:     transform.rotation,      direction:       direction,
						hitInfoDistance: hitInfo.distance,

						duration:        debugCastLifetime,
						color:           debugCastHit
					);
				}
			}
			#endif

			return new System.Nullable<RaycastHit>(hitInfo);
		} // End Cast



		override public RaycastHit[] CastAll(Vector3 direction, float distance) {

			RaycastHit[] hitInfoAr;

			hitInfoAr = Physics.BoxCastAll(
				center:       transform.position,   halfExtents:             castBoxSize,
				direction:    direction,            layermask:               collisionMask,
				orientation:  transform.rotation,   queryTriggerInteraction: QueryTriggerInteraction.Ignore,
				maxDistance:  distance
			);


			#if UNITY_EDITOR
			if(debugCasts) {
				ExtDebug.DrawBoxCastBox(
					origin:      transform.position, halfExtents: castBoxSize,
					orientation: transform.rotation, direction:   direction,
					distance:    distance,           

					duration:    debugCastLifetime,
					color:       (hitInfoAr.Length > 0 ? debugCastAttempt : debugCastMiss)
				);

				foreach(RaycastHit hitInfo in hitInfoAr) {
					ExtDebug.DrawBoxCastOnHit(
						origin:          transform.position,      halfExtents:     castBoxSize,
						orientation:     transform.rotation,      direction:       direction,
						hitInfoDistance: hitInfo.distance,

						duration:        debugCastLifetime,
						color:           debugCastHit
					);
				}
			}
			#endif

			return hitInfoAr;
		} // End CastAll


	} // End of class
} // End of namespace