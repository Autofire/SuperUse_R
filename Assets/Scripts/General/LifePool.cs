using UnityEngine;
using Teams;

public class LifePool : MonoBehaviour {

	[SerializeField] private Resource life;
	[SerializeField] private Team _myTeam;

	public Team myTeam {
		get { return _myTeam; }
	}

	private void Start() {
		life.Maximize();
	}

	/// <summary>
	/// Deduct health from this life pool based on the given amount. Only does so if the attacking team is enemies with
	/// this life pool's team.
	/// </summary>
	/// <param name="damage">Damage to be dealt. Should be positive unless doing some sort of benefficial "damage."</param>
	/// <param name="attackingTeam">Attacking team.</param>
	/// <param name="deltaHP">Amount the life total changed. Positive if life total decreased.</param>
	public bool Hurt(int damage, Team attackingTeam, out int deltaHP) {
		bool changed = false;
		deltaHP = 0;

		if(attackingTeam.IsMutualEnemy(myTeam)) {
			deltaHP = life.current;
			changed = life.Change(-damage);
			deltaHP -= life.current;
		}

		return changed;
	}

	/// <summary>
	/// Deduct health from this life pool based on the given amount. Only does so if the attacking team is enemies with
	/// this life pool's team.
	/// </summary>
	/// <param name="damage">Damage to be dealt. Should be positive unless doing some sort of benefficial "damage."</param>
	/// <param name="attackingTeam">Attacking team.</param>
	public bool Hurt(int damage, Team attackingTeam) {
		int tmp;

		return Hurt(damage, attackingTeam, out tmp);
	}

	/// <summary>
	/// Applies the given damage amount without caring about teams.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void Hurt(int damage) {
		life.Change(-damage);
	}

	/// <summary>
	/// Restore health to this life pool based on the given amount, so long as the healing team is not enemies with this
	/// team.
	/// </summary>
	/// <param name="healAmount">Amount to heal. Should be positive unless doing some kind of negative "heal."</param>
	/// <param name="healingTeam">Team doing the healing.</param>
	/// <param name="deltaHP">Amount the life total changed. Positive if life total increased.</param>
	public bool Heal(int healAmount, Team healingTeam, out int deltaHP) {
		bool changed = false;
		deltaHP = 0;

		if(!healingTeam.IsMutualEnemy(myTeam)) {
			deltaHP = life.current;
			changed = life.Change(healAmount);
			deltaHP = life.current - deltaHP;
		}

		return changed;
	}

	/// <summary>
	/// Restore health to this life pool based on the given amount, so long as the healing team is not enemies with this
	/// team.
	/// </summary>
	/// <param name="healAmount">Amount to heal. Should be positive unless doing some kind of negative "heal."</param>
	/// <param name="healingTeam">Team doing the healing.</param>
	public bool Heal(int healAmount, Team healingTeam) {
		int tmp;

		return Heal(healAmount, healingTeam, out tmp);
	}

	/// <summary>
	/// Heals by the specified amount without caring about teams.
	/// </summary>
	/// <param name="healAmount">Heal amount.</param>
	public void Heal(int healAmount) {
		life.Change(healAmount);
	}
}
