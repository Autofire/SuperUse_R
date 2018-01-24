using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Characters.Bodies;

namespace Characters.Brains.Players {

	[CreateAssetMenu(menuName="Brains/Player Brain")]
	public class PlayerBrain : BaseBrain {

		[SerializeField] string xAxisName;
		[SerializeField] string yAxisName;
		[SerializeField] string jumpButtonName;


		override public void Initialize(Bodies.BaseBody body) {
		}

		override public void Think(Bodies.BaseBody body) {
			IJump bodyJump = body as IJump;
			IMoveX bodyMoveX = body as IMoveX;

			if(bodyJump != null) {
				if(Input.GetButtonDown(jumpButtonName)) {
					bodyJump.JumpBegin();
				}
				else if(Input.GetButtonUp(jumpButtonName)) {
					bodyJump.JumpEnd();
				}
			}

			if(bodyMoveX != null) {
				bodyMoveX.MoveX(Input.GetAxis(xAxisName));
			}
		}

	}

}