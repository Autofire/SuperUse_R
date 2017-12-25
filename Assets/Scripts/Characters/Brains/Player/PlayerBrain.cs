using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Brains.Players {

	[CreateAssetMenu(menuName="Brains/Player Brain")]
	public class PlayerBrain : BaseBrain {

		[SerializeField] string xAxisName;
		[SerializeField] string yAxisName;
		[SerializeField] string jumpButtonName;


		override public void Initialize(Bodies.BaseBody body) {
			body.Remember<float>("Adder", 0f);
		}

		override public void Think(Bodies.BaseBody body) {

			float adder = body.Remember<float>("Adder");

			adder += Input.GetAxis(xAxisName);

			Debug.Log(adder);

			body.Remember<float>("Adder", adder);
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