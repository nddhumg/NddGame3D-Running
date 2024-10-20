using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[SerializeField]
public class ShieldEffect : BaseEffectUpdate {

	public ShieldEffect() : base(){
		this.nameEffect = EffectName.Shield;
		activeTime = 10f;
	}

	protected override void Enter ()
	{
		base.Enter ();
		player.Collision.OnCollision -= player.PlayerController.OnDead ;
		player.Collision.OnCollision += Shield;
		player.PlayerEffect.EquipmentShield.SetActive (true);
	}

	protected override void Exit ()
	{
		base.Exit ();
		player.Collision.OnCollision += player.PlayerController.OnDead ;
		player.Collision.OnCollision -= Shield;
		player.PlayerEffect.EquipmentShield.SetActive (false);
	}

	protected virtual void Shield(RaycastHit hit){
		hit.transform.gameObject.SetActive (false);
		ChangeState (State.Exit);
	}
}
