using UnityEngine;
using Characters.Bodies;
using VariableObjects;

namespace Characters.Brains.Players {

	[CreateAssetMenu(menuName="Brains/Platforming Hunter Brain")]
	public class PlatformingHunterBrain : BaseBrain {

		[SerializeField] TransformConstReference chaseTarget;
		[SerializeField] FloatConstReference moveDeadZone;
		[SerializeField] FloatConstReference turnDeadZone;

		override public void Initialize(Bodies.BaseBody body) {

		}

		override public void Think(Bodies.BaseBody body) {

			if(chaseTarget.constValue != null) {

				IJump bodyJump = body as IJump;
				IMoveX bodyMoveX = body as IMoveX;
				IHaveDirections directions = body as IHaveDirections;

				if(bodyMoveX != null) {


					if(body.transform.position.x - chaseTarget.constValue.position.x > 0) {
						bodyMoveX.MoveX(-1f);
					}
					else if(body.transform.position.x - chaseTarget.constValue.position.x < 0) {
						bodyMoveX.MoveX(1f);
					}


				}

				if(directions != null) {
					//Debug.Log(directions.forward);

					Vector3 heading = chaseTarget.constValue.position - body.transform.position;
					float dot = Vector3.Dot(heading, directions.forward);

					Debug.Log(dot);
					//Debug.Log(directions.right);
				}

			}
		}
	}

}