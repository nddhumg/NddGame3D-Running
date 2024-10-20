using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NddBehaviour : MonoBehaviour {

	// Use this for initialization
	
	protected virtual void Awake () {
		this.LoadComponent();
	}

	protected virtual void Reset(){
		Load ();
	}

	[ContextMenu("Load")]
	protected virtual void Load() {
		this.ResetValue();
		this.LoadComponent ();
		this.ResetValueComponent();
	}

	protected virtual void LoadComponent() {
        //Override
    }

	protected virtual void ResetValueComponent() {
		//Override
	}

    protected virtual void ResetValue() {
        //Override
	}

	protected virtual void DebugLoadComponent(string nameComponent){
		Debug.LogWarning ("Load " + nameComponent);
	}

	protected virtual void DebugLoadComponent(string nameComponent,GameObject obj){
		Debug.LogWarning ("Load " + nameComponent, obj);
	}
}    
