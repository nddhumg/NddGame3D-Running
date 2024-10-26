using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class BaseEffectPhysic : BaseEffect {

	public BaseEffectPhysic(GameObject ui): base(ui) {
	}

	public override void FixedUpdate(){
		StateHandle ();
	}

	public sealed override void Update(){

	}
}
