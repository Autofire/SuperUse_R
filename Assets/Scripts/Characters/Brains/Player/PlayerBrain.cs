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

			if(bodyJump != null) {
				if(Input.GetButtonDown(jumpButtonName)) {
					bodyJump.JumpBegin();
				}
				else if(Input.GetButtonUp(jumpButtonName)) {
					bodyJump.JumpEnd();
				}
			}
		}

		override public void FixedThink(Bodies.BaseBody body) {

			IMoveX bodyMoveX = body as IMoveX;

			if(bodyMoveX != null) {
				bodyMoveX.MoveX(Input.GetAxis(xAxisName));
			}
		}

		/*
		void FixedUpdate() {
			IssueCommand(OnMoveX, Input.GetAxis(xAxisName));
			IssueCommand(OnMoveY, Input.GetAxis(yAxisName));
		}

		void Update() {
			if(Input.GetButtonDown(jumpButtonName)) {
				IssueCommand(OnJumpBegin);
			}
			else if(Input.GetButtonUp(jumpButtonName)) {
				IssueCommand(OnJumpEnd);
			}
		}
		*/

	}

}