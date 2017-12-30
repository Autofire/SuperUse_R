using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhysics {

	abstract public class GameBody : MonoBehaviour {
		abstract public Vector3 Move(Vector3 motion);
	}
}