using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MagnetEffect : BaseEffectPhysic {

	public MagnetEffect(): base() {
		nameEffect = EffectName.Magnet;
	}

	protected override void Enter ()
	{
		base.Enter ();
		player.PlayerEffect.MagnetCollision.SetActive (true);
	}

	protected override void Exit ()
	{
		base.Exit ();
		player.PlayerEffect.MagnetCollision.SetActive (false);
	}


}
