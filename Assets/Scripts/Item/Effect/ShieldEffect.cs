using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[SerializeField]
public class ShieldEffect : BaseEffectUpdate {

	public ShieldEffect(GameObject ui) : base(ui){
		this.nameEffect = EffectName.Shield;
		activeTime = 10f;
	}

	protected override void Enter ()
	{
		base.Enter ();
		player.Collision.OnCollision -= player.PlayerController.Dead ;
		player.Collision.OnCollision += Shield;
		player.PlayerEffect.EquipmentShield.SetActive (true);
	}

	protected override void Exit ()
	{
		base.Exit ();
		player.Collision.OnCollision += player.PlayerController.Dead ;
		player.Collision.OnCollision -= Shield;
		player.PlayerEffect.EquipmentShield.SetActive (false);
	}

	protected virtual void Shield(){
		GameController.instance.ClearHindrance ();
		ChangeState (State.Exit);
	}
}
