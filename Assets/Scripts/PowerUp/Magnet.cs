using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Magnet : PowerUpPickUp {

	protected override void ResetValue ()
	{
		base.ResetValue ();
		effectName = EffectName.Magnet;
	}
}
