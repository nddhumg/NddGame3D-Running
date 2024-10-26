using UnityEngine;
using Player;

public class Shield : ItemPickUp {

	protected override void ResetValue ()
	{
		base.ResetValue ();
		effectName = EffectName.Shield;
	}

}
