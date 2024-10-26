using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderShield : SliderBaseEffect {

	void Start(){
		shield = Player.PlayerManager.instance.PlayerEffect.GetBaseEffectByName (EffectName.Shield);
	}
}
