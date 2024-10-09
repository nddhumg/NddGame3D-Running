using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NddBehaviour : MonoBehaviour {

	// Use this for initialization
	
	protected virtual void Start () {
		this.LoadComponent();
	}
	protected virtual void Awake(){
		this.LoadSingleton ();
	}
	// Update is called once per frame

	protected virtual void Reset() {
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
	protected virtual void LoadSingleton(){
		//Override
	}
}    
