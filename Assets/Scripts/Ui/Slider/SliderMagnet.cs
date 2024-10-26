using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMagnet : SliderBaseEffect {

	// Use this for initialization
	void Start(){
		shield = Player.PlayerManager.instance.PlayerEffect.GetBaseEffectByName (EffectName.Magnet);
	}
}
