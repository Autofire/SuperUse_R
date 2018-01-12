using UnityEngine;

namespace Teams {

	[DisallowMultipleComponent]
	public class TeamContainer : MonoBehaviour {

		[SerializeField] private Team _myTeam;

		public Team myTeam {
			get { return _myTeam; }
		}

	}

}
