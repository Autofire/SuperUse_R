using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnGameObjectFX : FXHandler {

	[Space(10)]
	[SerializeField] GameObject objectPrefab;
	[SerializeField] Transform spawnPosition;
	[SerializeField] Transform spawnRotation;
	[SerializeField] Transform spawnParent;

	GameObject spawnedObj = null;

	void Awake() {
		Assert.IsNotNull(objectPrefab);
	}

	public override void Play() {
		Stop();

		Vector3 targetPos = (spawnPosition != null ?
			spawnPosition.position : objectPrefab.transform.position);
		Quaternion targetRot = (spawnRotation != null ?
			spawnRotation.rotation : objectPrefab.transform.rotation);

		spawnedObj = Instantiate(objectPrefab, targetPos, targetRot, spawnParent) as GameObject;
	}

	public override void Stop() {
		if(spawnedObj != null) {
			Destroy(spawnedObj);
		}
	}

	public override bool IsPlaying() {
		return spawnedObj != null;
	}
}
