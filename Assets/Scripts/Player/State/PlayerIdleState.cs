using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState {
	private PlayerStateMachine manager;

	public PlayerIdleState(PlayerStateMachine manager){
		this.manager = manager;
	}

	public virtual void Enter (){
		manager.anim.SetBool ("isMove", true);
	}

	public virtual void Exit(){
		manager.anim.SetBool ("isMove", false);

	}

	public virtual void UpdateLogic(){
		if (manager.input.UpTouch) {
			manager.ChangeState (manager.jumpState);
		}
		else if (manager.input.DownTouch){
			manager.ChangeState (manager.rollState);
		}
	}

	public virtual void UpdatePhysics(){

	}
}
