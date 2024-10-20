using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class BaseEffectPhysic : BaseEffect {

	public BaseEffectPhysic(): base() {
	}

	public override void FixedUpdate(){
		StateHandle ();
	}

	public sealed override void Update(){

	}
}
