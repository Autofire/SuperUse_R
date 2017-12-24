using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Brains.Players {

	public class PlayerBrain : BaseBrain, IOnMoveX, IOnMoveY, IOnJumpBegin, IOnJumpEnd {

		[SerializeField] string xAxisName;
		[SerializeField] string yAxisName;
		[SerializeField] string jumpButtonName;

		public event MagnitudeCommand OnMoveX;
		public event MagnitudeCommand OnMoveY;
		public event Command OnJumpBegin;
		public event Command OnJumpEnd;

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

	}

}