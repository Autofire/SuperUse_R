using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters.Bodies {

	[RequireComponent(typeof(GamePhysics.GameRigidBody))]
	public class PlatformerBody : BaseBody, IMoveX, IStand, IJump {

		[SerializeField] TriggerObserver footBox;
		[SerializeField] GamePhysics.GameRigidBody gBody;

		[Space(10)]
		[Tooltip(
			"Walking speed in units per second. " +
			"Keep in mind that this is the same even in the air."
		)]
		[Range(0f, 50f)]
		[SerializeField] float walkingSpeed = 1f;

		[Tooltip(
			"How high the character goes at the peek of a full jump. " +
			"Making this zero disables jumps."
		)]
		[Range(0f, 10f)]
		[SerializeField] float jumpHeight;

		[Tooltip(
			"How far the character goes at the peek of a full jump. " +
			"Making this zero disables jumps. " +
			"Higher values make jumps feel more snappy."
		)]
		[Range(0f, 10f)]
		[SerializeField] float jumpPeekDist;

		[Tooltip(
			"This determines how fast the character falls " +
			"either after they fall of an edge or reach the peek of a jump. " +
			"This is effectively a scaler of the 'jump gravity' which gets " +
			"calculated based on the values above."
		)]
		[Range(0f, 10f)]
		[SerializeField] float normalGravityScale = 3f;

		Vector2 velocity;
		float gravity;
		//bool isJumping;

		/// <summary>
		/// Gets the magnitude of the jump velocity.
		/// </summary>
		/// <value>The jump velocity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpVelocity {
			get { return 2 * jumpHeight * walkingSpeed / jumpPeekDist; }
		}

		/// <summary>
		/// Gets the magnitude of the jump gravity.
		/// </summary>
		/// <value>The jump gravity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpGravity {
			get { return -2 * jumpHeight * walkingSpeed * walkingSpeed / (jumpPeekDist * jumpPeekDist); }
		}

		/// <summary>
		/// Gravity which the character experiences normally.
		/// </summary>
		/// <value>The normal gravity.</value>
		float normalGravity {
			get { return normalGravityScale * jumpGravity; }
		}

		#region Unity events
		override protected void Awake() {
			base.Awake();

			Assert.IsNotNull(gBody);

			velocity = Vector3.zero;
			gravity = normalGravity;
		}

		override protected void Update() {
			base.Update();

			velocity.y += (gravity * Time.deltaTime);
			velocity = ApplyVelocity(velocity);

			if(velocity.y <= 0f) {
				gravity = normalGravity;
			}
		}
		#endregion


		#region Helper functions
		Vector2 ApplyVelocity(Vector2 velocity) {
			/*
			Vector3 hMotion = Vector3.right * velocity.x;
			Vector3 vMotion = Vector3.up * velocity.y;

			Vector3 hMotionFinal = gBody.Move(hMotion * Time.deltaTime) / Time.deltaTime;
			Vector3 vMotionFinal = gBody.Move(vMotion * Time.deltaTime) / Time.deltaTime;

			//Debug.Log(vMotion.ToString("F5") + vMotionFinal.ToString("F5") + new Vector2(hMotionFinal.magnitude, vMotionFinal.magnitude).ToString());

			return new Vector2(
				hMotionFinal.magnitude * Mathf.Sign(velocity.x),
				vMotionFinal.magnitude * Mathf.Sign(velocity.y)
			);
*/
			Vector3 motion = new Vector3(velocity.x, velocity.y);
			motion = gBody.Move(motion * Time.deltaTime) / Time.deltaTime;
			return new Vector2(motion.x, motion.y);
		}
		#endregion

		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			//velocity.x = walkingSpeed * magnitude;

			gBody.Move(Vector3.right * (magnitude * walkingSpeed * Time.deltaTime));
		}

		public void JumpBegin ()
		{
			if(IsStanding()) {
				//isJumping = true;

				gravity = jumpGravity;
				velocity.y += jumpVelocity;
			}

		}
		public void JumpEnd ()
		{
			gravity = normalGravity;
		}

		public bool IsStanding() {
			return footBox.isTriggered;
		}
		#endregion

	}

}