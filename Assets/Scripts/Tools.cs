using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReachBeyond {

	/// <summary>
	/// These are generic tools that should be useful for pretty much any game.
	/// </summary>
	public static class Tools {

		#region Direction tools
		public enum Direction3D : int {
			Up=0,
			Down,
			Left,
			Right,
			Forward,
			Backward
		}
		static string[] direction3DNames = {"up", "down", "left", "right", "forward", "backward"};

		public enum Direction2D : int {
			Up=0,
			Down,
			Left,
			Right
		}
		static string[] direction2DNames = {"up", "down", "left", "right"};

		public static string DirectionToString(Direction2D dir) {
			return direction2DNames[(int)dir];
		}

		public static string DirectionToString(Direction3D dir) {
			return direction3DNames[(int)dir];
		}

		public static Direction2D StringToDirection(string dir) {

			dir = dir.ToLower();

			for(int i = 0; i < direction2DNames.Length; i++) {
				if(direction2DNames[i].Equals(dir)) {
					return (Direction2D)i;
				}
			}

			return Direction2D.Up;
		}
		#endregion


		#region Input
		/// <summary>
		/// Determines if is mouse in the window.
		/// </summary>
		/// <returns><c>true</c> if is mouse in window; otherwise, <c>false</c>.</returns>
		public static bool IsMouseInWindow() {
			bool withinWidth  = Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width;
			bool withinHeight = Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height;

			return withinWidth && withinHeight;
		}
		#endregion


		#region GameObject manipulation
		/// <summary>
		/// Sends the message to all objects in the list.
		/// </summary>
		/// <param name="methodName">Name of message to send.</param>
		/// <param name="targets">Targets to send message to.</param>
		/// <param name="sender">Sender of message. Use if [sender] may be in [targets].</param>
		public static void SendMessageToAll(
			string messageName,
			GameObject[] targets,
			GameObject sender = null)
		{
			foreach(GameObject target in targets) {
				if(target != sender && target != null) {
					target.SendMessage(messageName);
				}
			} // End foreach(GameObject target in targets)
		} // End SendMessageToAll(...)

		/// <summary>
		/// Sends the message to all objects in the list.
		/// </summary>
		/// <param name="methodName">Name of message to send.</param>
		/// <param name="targets">Targets to send message to.</param>
		/// <param name="sender">Sender of message. Use if [sender] may be in [targets].</param>
		/// <typeparam name="T">Type of component to send message to.</typeparam>
		public static void SendMessageToAllComponents<T>(
			string messageName,
			GameObject[] targets,
			GameObject sender = null)
			where T: Component
		{
			foreach(GameObject target in targets) {
				if(target != sender && target != null) {
					T[] components = target.GetComponents<T>();

					foreach(T component in components) {
						component.SendMessage(messageName);
					}
				}
			} // End foreach(GameObject target in targets)
		} // End SendMessageToAll(...)
		#endregion


		#region Comparison tools
		public enum CompareMode {
			Equals,
			NotEquals,
			LessThan,
			LessThanOrEqualTo,
			GreaterThan,
			GreaterThanOrEqualTo
		}

		/// <summary>
		/// Compares val1 and val2 using the specified CompareMode. Note that
		/// Tools.CompareMode.Equals uses Mathf.Approximately().
		/// </summary>
		/// <returns>True if comparison succeeded; false otherwise.</returns>
		public static bool Compare(float val1, float val2, CompareMode mode) {

			switch(mode) {
			case CompareMode.Equals:
				return Mathf.Approximately(val1, val2);
			case CompareMode.NotEquals:
				return val1 != val2;
			case CompareMode.LessThan:
				return val1 < val2;
			case CompareMode.LessThanOrEqualTo:
				return val1 <= val2;
			case CompareMode.GreaterThan:
				return val1 > val2;
			case CompareMode.GreaterThanOrEqualTo:
				return val1 >= val2;
			default:
				return false;
			}

		}
		#endregion


		#region Vector3 manipulation tools

		[System.Serializable]
		public struct Bool3 {
			public Bool3(bool x, bool y, bool z) {
				this.x = x;
				this.y = y;
				this.z = z;
			}

			public bool x;
			public bool y;
			public bool z;
		}

		/// <summary>
		/// This is not a real multiplication; does v3.x = v1.x * v2.x, for example.
		/// </summary>
		public static Vector3 MultiplyComponents(Vector3 v1, Vector3 v2) {
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}

		public static Vector3 Abs(Vector3 v) {
			return new Vector3(
				Mathf.Abs(v.x),
				Mathf.Abs(v.y),
				Mathf.Abs(v.z)
			);
		}

		/// <summary>
		/// Selectively replace values in *current* with those in *replace*.
		/// Especially useful with replace being Vector3.zero.
		/// </summary>
		/// <returns>Merged Vector3.</returns>
		/// <param name="current">Base values.</param>
		/// <param name="replace">Replacement values.</param>
		/// <param name="replaceX">If set to <c>true</c>, replace current.x.</param>
		/// <param name="replaceY">If set to <c>true</c>, replace current.y.</param>
		/// <param name="replaceZ">If set to <c>true</c>, replace current.z.</param>
		public static Vector3 ReplaceValues(
			Vector3 current, Vector3 replace,
			bool replaceX, bool replaceY, bool replaceZ)
		{
			bool[] replaceAxis = new bool[] {replaceX, replaceY, replaceZ};

			for(int axis = 0; axis < 3; axis++) {
				if(replaceAxis[axis]) {
					current[axis] = replace[axis];
				}
			}

			return current;
		}

		public static Vector3 ReplaceValues(Vector3 current, Vector3 replace, Bool3 mask) {
			return ReplaceValues(current, replace, mask.x, mask.y, mask.z);
		}

		/// <summary>
		/// "Moves" basePosition so that basePosition.y equals targetY without
		/// making it appear as if the object moved at all. 
		/// </summary>
		/// <returns>The new position.</returns>
		/// <param name="currentPosition">Position that will be moved.</param>
		/// <param name="targetY">Target y position.</param>
		public static Vector3 MoveRelativeToCamera(Vector3 currentPosition, float targetY) {
			Vector3 axis = Camera.main.transform.forward;

			float scaleFactor = (targetY - currentPosition.y) / axis.y;

			return new Vector3(
				currentPosition.x + scaleFactor * axis.x,
				targetY,
				currentPosition.z + scaleFactor * axis.z
			);
		}

		/// <summary>
		/// This is a wrapper for a Vector3 that is serializeable by C#'s
		/// serialization routines.
		/// </summary>
		[System.Serializable]
		public class SerializedVector3 : System.Object {

			// Please note that making this a struct is a pain in the arse.

			public SerializedVector3()
				: this(Vector3.zero) {}
			public SerializedVector3(float x, float y)
				: this(x, y, 0f) {}
			public SerializedVector3(float x, float y, float z)
				: this(new Vector3(x, y, z)) {}
			public SerializedVector3(Vector3 initVal) {
				value = initVal;
			}

			private float xOffset;
			private float yOffset;
			private float zOffset;

			public Vector3 value {
				get { return new Vector3(xOffset, yOffset, zOffset); }
				set {
					xOffset = value.x;
					yOffset = value.y;
					zOffset = value.z;
				}
			}
		}
		#endregion


		#region Transform manipulation tools
		// TODO Make a conversion function to go from this to Direction3D
		public enum TransformDirection {
			Up,
			Right,
			Forward
		}

		public static Vector3 GetDirectionVector(Transform transform, TransformDirection dir) {
			switch(dir) {
			case TransformDirection.Up:
				return transform.up;
			case TransformDirection.Right:
				return transform.right;
			case TransformDirection.Forward:
			default:
				return transform.forward;
			}
		}

		/// <summary>
		/// Rotates myTransform towards targetPos with the given speed.
		/// </summary>
		/// <param name="myTransform">Transform of object rotating.</param>
		/// <param name="targetTransform">Position to look at.</param>
		/// <param name="rotationSpeed">Speed to rotate, in degrees or radians.</param>
		/// <param name="useDegrees">If set to <c>true</c> use degrees, otherwise radians.</param>
		public static void RotateTowards(
			Transform myTransform,
			Vector3 targetPos,
			float rotationSpeed,
			bool useDegrees = true)
		{
			Vector3 targetDir;
			Vector3 newDir;
			float	step;			// Amount of change for this cycle

			// This code basically comes out of the RotateTowards documentation.
			targetDir = targetPos - myTransform.position;
			step = rotationSpeed * Time.deltaTime * (useDegrees ? Mathf.Deg2Rad : 1f);
			newDir = Vector3.RotateTowards(myTransform.forward, targetDir, step, 0.0F);
			myTransform.rotation = Quaternion.LookRotation(newDir);
		}

		/// <summary>
		/// Rotates myTransform towards targetTransform with the given speed.
		/// </summary>
		/// <param name="myTransform">Transform of object rotating.</param>
		/// <param name="targetTransform">Transform to look at.</param>
		/// <param name="rotationSpeed">Speed to rotate, in degrees or radians.</param>
		/// <param name="useDegrees">If set to <c>true</c> use degrees, otherwise radians.</param>
		public static void RotateTowards(
			Transform myTransform,
			Transform targetTransform,
			float rotationSpeed,
			bool useDegrees = true)
		{
			RotateTowards(myTransform, targetTransform.position, rotationSpeed, useDegrees);
		}
		#endregion

	} // End class Tools

} // End namespace