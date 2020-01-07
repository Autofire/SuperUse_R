using UnityEngine;
using VariableObjects;


public class SpawnAndAlignObject : MonoBehaviour {

	[SerializeField] private GameObjectConstReference objPrefab;
	[SerializeField] private string spawnAreaTag;
	[SerializeField] private FloatConstReference timeOutDelay;

	private float startTime;

	void OnEnable() {
		startTime = Time.time;
	}

	void Update() {
		if(Time.time > startTime + timeOutDelay.constValue) {
			MortalityHandler.Kill(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {

		if(enabled) {
			SpawnField field = other.GetComponent<SpawnField>();

			if(field != null && other.CompareTag(spawnAreaTag)) {
				Vector3 position = field.AlignPosition(transform.position);
				Quaternion rotation = field.AlignRotation(transform.up);

#pragma warning disable CS0219
//#pragma warning disable CS1692
				GameObject newObject = Instantiate(
					objPrefab.constValue,
					position,
					rotation,
					null
				) as GameObject;
#pragma warning restore

				MortalityHandler.Kill(gameObject);
			}
		}
	}

} // End class

