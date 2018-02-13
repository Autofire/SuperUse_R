using UnityEngine;
using Characters.Bodies;
using VariableObjects;
using System;

namespace Characters.Brains.Players {

	[CreateAssetMenu(menuName="Brains/Patrol AntiVirus Brain")]
	public class PatrolAntiVirusBrain : BaseBrain {

		[SerializeField] FloatConstReference patrolLength;
		[SerializeField] FloatConstReference turnDelay;

		[SerializeField] FloatConstReference moveDeadZone;
		[SerializeField] FloatConstReference turnDeadZone;

		override public void Initialize(Bodies.BaseBody body) {

			body.InitMemory();

			body.Remember("patrolStart", body.transform.position);
			body.Remember("patrolEnd", body.transform.position + new Vector3(patrolLength.constValue, 0, 0));
			//body.Remember("currentTarget", body.Remember<Vector3>("patrolEnd"));
			body.Remember("goingToEnd", true);
		}

		public override void Think(BaseBody body) {

			IMoveX bodyMoveX = body as IMoveX;
			//IHaveDirections directions = body as IHaveDirections;

			Vector3 currentTarget;

			if(bodyMoveX != null) {

				if(body.Remember<bool>("goingToEnd")) {
					currentTarget = body.Remember<Vector3>("patrolEnd");
				}
				else {
					currentTarget = body.Remember<Vector3>("patrolStart");
				}

				if(Mathf.Abs(body.transform.position.x - currentTarget.x) <= moveDeadZone.constValue) {
				//if(Vector3.Magnitude(body.transform.position - currentTarget) < moveDeadZone.constValue) {

					Debug.Log("Switch Target");

					if (body.Remember<bool>("goingToEnd")) {

						Debug.Log("Switch to Start");
						body.Remember("goingToEnd", false);
					}
					else {

						Debug.Log("Switch to End");
						body.Remember("goingToEnd", true);
					}
				}


				if (body.transform.position.x - currentTarget.x > moveDeadZone.constValue) {
					bodyMoveX.MoveX(-1f);
				}
				else if (body.transform.position.x - currentTarget.x < moveDeadZone.constValue) {
					bodyMoveX.MoveX(1f);
				}
			
				Debug.Log(currentTarget);

			}

			//if(directions != null) {

			//	Vector3 heading = body.Remember<Vector3>("currentTarget") - body.transform.position;
			//	float dot = Vector3.Dot(heading, directions.forward);
			//}

		}

	}
}
 