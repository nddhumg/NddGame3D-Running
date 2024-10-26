using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBaseEffect : SliderBase {
	[SerializeField] protected BaseEffect shield;

	void Update(){
		slider.value = shield.Timer / shield.ActiveTime;
	}

}
