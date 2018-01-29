using UnityEngine;

// This component spawns a set of GameObjects on create. The objects are parented to the
// GameObject that this is attached to, and then this script vanishes.
public class ObjectSpawner : MonoBehaviour {

	public Transform targetParent;
	// TODO Figure out why using GameObjectConstReference doesn't work
	public GameObject[] objList;

	// Use this for initialization
	void Awake () {

		foreach(GameObject obj in objList) {
			Instantiate(obj, transform.position, transform.rotation, targetParent);
		}

		Destroy(this);
	}
}
