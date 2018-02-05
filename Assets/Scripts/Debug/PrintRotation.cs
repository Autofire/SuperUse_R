using UnityEngine;

namespace ReachBeyond.Debug {
	public class PrintRotation : MonoBehaviour {

		void Update () {
			UnityEngine.Debug.Log(transform.rotation);
		}
	}
}