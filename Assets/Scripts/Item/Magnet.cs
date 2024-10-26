using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Magnet : ItemPickUp {

	protected override void ResetValue ()
	{
		base.ResetValue ();
		effectName = EffectName.Magnet;
	}
}
