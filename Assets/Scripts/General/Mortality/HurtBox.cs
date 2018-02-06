using UnityEngine;
using Teams;
using VariableObjects;

public class HurtBox : MonoBehaviour {

	[SerializeField] private Team myTeam;
	[SerializeField] private IntConstReference damage;

	void OnTriggerEnter(Collider other) {
		LifePool pool = other.GetComponent<LifePool>();

		if(pool != null && myTeam.IsMutualEnemy(pool.myTeam)) {
			pool.Hurt(damage.constValue);
		}
	}
	
}
