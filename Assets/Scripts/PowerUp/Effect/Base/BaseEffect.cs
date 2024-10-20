
using UnityEngine;
using System;

[SerializeField]
public class BaseEffect  {
	protected Player.PlayerManager player;
	protected float timer = 0;
	protected float activeTime = 10f;
	private State currentState = State.None;
	protected EffectName nameEffect;

	public enum State{
		None,
		Enter,
		Exit,
		Update,
	}

	public State CurrentState{
		get{
			return currentState;	
		}
	}

	public EffectName EffectName{
		get{ 
			return nameEffect;
		}
	}

	public BaseEffect() {
		this.player = Player.PlayerManager.instance;
	}

	public virtual void Update(){
		
	}

	public virtual void FixedUpdate(){
		
	}
		

	public virtual bool IsEffectActive(){
		return timer < activeTime;
	}

	public virtual void Reset(){
		ChangeState (State.Enter);
	}

	protected virtual void Enter(){
		timer = 0;
	}

	protected virtual void Exit(){
	
	}

	protected virtual void LogicUpdate(){
		
	}

	protected virtual void ChangeState(State newState){
		currentState = newState;
	}

	protected void StateHandle(){
		switch (CurrentState) {
		case State.Enter:
			Enter ();
			ChangeState (State.Update);
			break;
		case State.Update:
			timer += Time.deltaTime;
			if (!IsEffectActive ()) {
				ChangeState (State.Exit);
			} else {
				LogicUpdate ();
			}
			break;
		case State.Exit:
			Exit ();
			ChangeState (State.None);
			break;
		}
	}
}
