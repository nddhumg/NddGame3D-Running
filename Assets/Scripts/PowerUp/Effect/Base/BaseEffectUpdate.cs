using UnityEngine;

[SerializeField]
public class BaseEffectUpdate : BaseEffect {

	public BaseEffectUpdate(): base() {
	}
	
	public override void Update(){
		StateHandle ();
	}

	public sealed override void FixedUpdate(){
		
	}
}
