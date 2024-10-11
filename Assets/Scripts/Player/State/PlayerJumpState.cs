using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState {
	private PlayerStateMachine manager;

	private float countJump = 3f;
	private bool isJump;

	private float jumpTime = 0.1f;
	private float timer = 0f;

	public PlayerJumpState(PlayerStateMachine manager){
		this.manager = manager;
	}

	public virtual void Enter (){
		isJump = true;
		manager.anim.SetBool ("isJump",true);
		manager.velocity.y = countJump;
	}

	public virtual void Exit(){
		isJump = false;
		timer = 0;
		manager.velocity.y = 0f;
		manager.anim.SetBool ("isJump",false);
	}

	public virtual void UpdateLogic(){
		if (manager.input.DownTouch) {
			manager.ChangeState (manager.rollState);
			return;
		}
		if (manager.player.IsGrounded && !isJump) {
			manager.ChangeState (manager.idleState);
			return;
		}
	}

	public virtual void UpdatePhysics(){
		if (timer >= jumpTime) {
			manager.velocity.y = 0;
			isJump = false;
		} else {
			timer += Time.fixedDeltaTime;
			manager.velocity.y = countJump * Time.fixedDeltaTime;
		}	
	}
}
