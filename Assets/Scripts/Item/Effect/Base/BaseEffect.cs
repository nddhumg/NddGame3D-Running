
using UnityEngine;
using System;

[SerializeField]
public class BaseEffect  {
	protected Player.PlayerManager player;
	protected float timer = 0;
	protected float activeTime = 10f;
	private State currentState = State.None;
	protected EffectName nameEffect;
	protected GameObject uiShow;

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

	public float Timer{
		get{
			return timer;	
		}
	}

	public float ActiveTime{
		get{
			return activeTime;	
		}
		set{ 
			activeTime = value;
		}
	}

	public BaseEffect(GameObject ui) {
		this.player = Player.PlayerManager.instance;
		this.uiShow = ui;
		this.uiShow.SetActive (false);
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

	public virtual void End(){
		if (currentState == State.None) {
			return;
		}
		Exit ();
		ChangeState (State.None);
	}

	protected virtual void Enter(){
		timer = 0;
		uiShow.SetActive (true);
	}

	protected virtual void Exit(){
		uiShow.SetActive (false);
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
