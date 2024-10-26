using UnityEngine;

[SerializeField]
public class BaseEffectUpdate : BaseEffect {

	public BaseEffectUpdate(GameObject ui): base(ui) {
	}
	
	public override void Update(){
		StateHandle ();
	}

	public sealed override void FixedUpdate(){
		
	}
}
