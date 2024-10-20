using UnityEngine;
using Player;

public class Shield : PowerUpPickUp {

	protected override void ResetValue ()
	{
		base.ResetValue ();
		effectName = EffectName.Shield;
	}

}
