using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
	public IState currentState;

	public virtual void Initialize(IState state){
		currentState = state;
		currentState.Enter();
	}

	public virtual void Update(){
		currentState.UpdateLogic ();
	}

	public virtual void FixedUpdate(){
		currentState.UpdatePhysics ();
	}

	public virtual void ChangeState(IState newState){
		currentState.Exit ();
		currentState = newState;
		currentState.Enter ();
	}
}
