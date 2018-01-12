using UnityEngine;
using System.Linq;

namespace Teams {

	[CreateAssetMenu(menuName="Other/Team")]
	public class Team : ScriptableObject {

		[SerializeField] private Team[] enemies;

		public bool IsMutualEnemy(Team other) {
			return enemies.Contains(other) || other.enemies.Contains(this);
		}

	}

}