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
    }

	protected void  LoadScript<T>(ref T script, GameObject obj) where T : class{
		if (script != null)
			return;
		script = obj.GetComponent<T> ();
		DebugLoadComponent (typeof(T).Name);
	}

	protected void LoadScript<T>(ref T script, Transform obj) where T : class{
		if (script != null)
			return;
		script = obj.GetComponent<T> ();
		DebugLoadComponent (typeof(T).Name);
	}

	protected void LoadScript<T>(ref T script) where T : class{
		if (script != null)
			return;
		script = gameObject.GetComponent<T> ();
		DebugLoadComponent (typeof(T).Name);
	}

	protected void LoadScriptInChild<T>(ref T script) where T : class{
		if (script != null)
			return;
		script = gameObject.GetComponentInChildren<T> ();
		DebugLoadComponent (typeof(T).Name);
	}

	protected virtual void ResetValueComponent() {
		//Override
	}

    protected virtual void ResetValue() {
        //Override
	}

	protected virtual void DebugLoadComponent(string nameComponent){
		Debug.LogWarning ("Load Script " + nameComponent, gameObject);
	}
}    
