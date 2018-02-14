using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;

namespace Characters.Bodies {

	public class FloatingBody : BaseBody, IMoveX, IMoveY {

		[SerializeField] GamePhysics.GameRigidBody gBody;

		[Space(10)]
		[SerializeField] FloatConstReference dampingScale;
		[SerializeField] FloatConstReference moveSpeedX;
		[SerializeField] FloatConstReference moveSpeedY;


		#region Unity events

		protected override void Awake() {
			base.Awake();

			Assert.IsNotNull(gBody);
		}

		protected override void OnEnable() {
			base.OnEnable();

			gBody.velocity = Vector2.zero;
		}

		protected override void Update() {
			base.Update();

			if(dampingScale.constValue != 0f) {
				gBody.velocity = new Vector3(
					Mathf.MoveTowards(gBody.velocity.x, 0, dampingScale.constValue * Time.deltaTime),
					Mathf.MoveTowards(gBody.velocity.y, 0, dampingScale.constValue * Time.deltaTime),
					Mathf.MoveTowards(gBody.velocity.z, 0, dampingScale.constValue * Time.deltaTime)
				);
			}

			Debug.Log(gBody.velocity);
		}

		#endregion

		#region IMoveX implementation

		public void MoveX (float magnitude) {
			//gBody.velocity = gBody.velocity + (Vector3.right * magnitude * moveSpeedX.constValue);
			gBody.velocity = new Vector3(
				magnitude * moveSpeedX.constValue,
				gBody.velocity.y,
				gBody.velocity.z
			);

		}

		#endregion


		#region IMoveY implementation

		public void MoveY (float magnitude) {
			//gBody.velocity = gBody.velocity + (Vector3.up * magnitude * moveSpeedY.constValue);
			gBody.velocity = new Vector3(
				gBody.velocity.x,
				magnitude * moveSpeedY.constValue,
				gBody.velocity.z
			);
		}

		#endregion



	}
}
