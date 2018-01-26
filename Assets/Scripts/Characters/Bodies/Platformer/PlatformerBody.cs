using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Characters.Bodies {

	[RequireComponent(typeof(GamePhysics.GameRigidBody))]
	public class PlatformerBody : BaseBody, IPlatformer, IHaveDirections {

		[SerializeField] GamePhysics.GameRigidBody gBody;

		[Space(10)]
		[Range(0f, 1f)]
		[SerializeField] float footBoxDepth = 0.01f;

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
		[SerializeField] float _jumpHeight;

		[Tooltip(
			"How far the character goes at the peek of a full jump. " +
			"Making this zero disables jumps. " +
			"Higher values make jumps feel more snappy."
		)]
		[Range(0f, 10f)]
		[SerializeField] float _jumpPeekDist;

		[Tooltip(
			"This determines how fast the character falls " +
			"either after they fall of an edge or reach the peek of a jump. " +
			"This is effectively a scaler of the 'jump gravity' which gets " +
			"calculated based on the values above."
		)]
		[Range(0f, 10f)]
		[SerializeField] float normalGravityScale = 3f;

		[Header("Animation")]
		[SerializeField] Transform rotationTarget;
		[SerializeField] Vector3 rightRotation;
		[SerializeField] Vector3 leftRotation;
		[Tooltip("If true, the reported 'forward' vector is inverted.")]
		[SerializeField] bool invertForward;
		[Tooltip("If true, the reported 'right' vector is inverted.")]
		[SerializeField] bool invertRight;
		[Tooltip("If true, the reported 'up' vector is inverted.")]
		[SerializeField] bool invertUp;

		[Space(10)]
		[SerializeField] Animator animator;
		[SerializeField] string moveFloatName     = "Forward";
		[SerializeField] string groundBoolName    = "OnGround";
		[SerializeField] string verticalFloatName = "Vertical"; 
		[SerializeField] string jumpTriggerName   = "Jumped";
		[Range(0f,1f)]
		[SerializeField] float jumpAnimScale = 1f;
		[Tooltip("If true, the will appear to walk even if something stops us from walking.")]
		[SerializeField] bool animateByIntendedSpeed = true;

		float gravity;

		#region Properties

		public float jumpMaxHeight {
			get { return _jumpHeight; }
		}

		public float jumpPeekDist {
			get { return _jumpPeekDist; }
		}
			
		public float jumpMaxDist {
			get {
				Debug.LogWarning(this.GetType().Name + " has not fully implemented jumpMaxDist; using jumpPeekDist instead");
				return _jumpPeekDist - (0);
			}
		}

		public Vector3 forward {
			get {
				if(!invertForward) {
					return rotationTarget.forward;
				}
				else {
					return -rotationTarget.forward;
				}
			}
		}

		public Vector3 right {
			get {
				if(!invertRight) {
					return rotationTarget.right;
				}
				else {
					return -rotationTarget.right;
				}
			}
		}

		public Vector3 up {
			get {
				if(!invertUp) {
					return rotationTarget.up;
				}
				else {
					return -rotationTarget.up;
				}
			}
		}

		/// <summary>
		/// Gets the magnitude of the jump velocity.
		/// </summary>
		/// <value>The jump velocity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpVelocity {
			get { return 2 * jumpMaxHeight * walkingSpeed / jumpPeekDist; }
		}

		/// <summary>
		/// Gets the magnitude of the jump gravity.
		/// </summary>
		/// <value>The jump gravity.</value>
		/// <seealso cref="https://www.youtube.com/watch?v=hG9SzQxaCm8"/>
		float jumpGravity {
			get { return -2 * jumpMaxHeight * walkingSpeed * walkingSpeed / (jumpPeekDist * jumpPeekDist); }
		}

		/// <summary>
		/// Gravity which the character experiences normally.
		/// </summary>
		/// <value>The normal gravity.</value>
		float normalGravity {
			get { return normalGravityScale * jumpGravity; }
		}

		#endregion

		#region Unity events
		override protected void Awake() {
			base.Awake();

			Assert.IsNotNull(gBody);

			gravity = normalGravity;
		}

		override protected void Update() {
			base.Update();

			if(animator != null) {
				animator.SetFloat(verticalFloatName, gBody.velocity.y * jumpAnimScale);
				animator.SetBool(groundBoolName, IsStanding());
			}

			gBody.velocity += Vector3.up * (gravity * Time.deltaTime);

			if(gBody.velocity.y <= 0f) {
				gravity = normalGravity;
			}

		}
		#endregion


		#region Interface implenetations
		public void MoveX(float magnitude) {
			magnitude = Mathf.Clamp(magnitude, -1f, 1f);

			Vector3 moveAmount = gBody.Move(Vector3.right * (magnitude * walkingSpeed * Time.deltaTime));

			if(animator != null) {
				animator.SetFloat(
					moveFloatName,
					animateByIntendedSpeed ? Mathf.Abs(magnitude) : Mathf.Abs(moveAmount.x) / Time.deltaTime
				);
			}

			if(rotationTarget != null) {
				if(magnitude > 0f) {
					rotationTarget.localRotation = Quaternion.Euler(rightRotation);
				}
				else if(magnitude < 0f) {	// Yes we make this check too; otherwise we'd flip when we hold still.
					rotationTarget.localRotation = Quaternion.Euler(leftRotation);
				}
			}
		}

		public void JumpBegin () {
			if(IsStanding()) {
				//isJumping = true;

				gravity = jumpGravity;
				gBody.velocity = new Vector3(gBody.velocity.x, jumpVelocity, gBody.velocity.z);
			}

			if(animator != null) {
				animator.SetTrigger(jumpTriggerName);
			}
		}
		public void JumpEnd ()
		{
			gravity = normalGravity;

			if(animator != null) {
				animator.ResetTrigger(jumpTriggerName);
			}
		}

		public bool IsStanding() {
			RaycastHit[] hitInfo = gBody.CastAll(transform.up * -footBoxDepth);

			return (hitInfo.Length > 0);
		}
		#endregion

	}

}