using UnityEngine;
using UnityEngine.Assertions;
using VariableObjects;
using System.Collections.Generic;

public class ExitController : MonoBehaviour {

	[SerializeField] private IntReference pickupCount;
	[SerializeField] private GameObject closedObject;
	[SerializeField] private GameObject openedObject;
	[SerializeField] private GameObjectConstReference trailPrafab;

	private List<GameObject> trails;
	
	void Awake() {
		Assert.IsNotNull(closedObject);
		Assert.IsNotNull(openedObject);
		Assert.IsNotNull(trailPrafab.constValue);

		pickupCount.value = 0;
	}
	
	public void RefreshStatus() {
		int remainingCount = pickupCount.constValue;
		bool opened = remainingCount == 0;

		// Toggle which door is opened
		openedObject.SetActive(opened);
		closedObject.SetActive(!opened);

		// Manage trails
		if(trails == null) {
			trails = new List<GameObject>();
		}

		while(remainingCount != trails.Count) {
			if(remainingCount < trails.Count) {
				// We have too many
				GameObject marked = trails[0];
				trails.RemoveAt(0);

				MortalityHandler.Kill(marked);
			}
			else {
				GameObject newTrail = Instantiate(
					trailPrafab.constValue,
					transform.position,
					transform.rotation,
					transform
				) as GameObject;

				trails.Add(newTrail);
			}
		}
	}
}
