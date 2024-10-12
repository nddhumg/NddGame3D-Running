using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
	public class PlayerMoveState : IState {
		private PlayerStateMachine manager;
		private string animationParameters = AnimationParameters.isMove.ToString();
		private string floatParameters = AnimationParameters.speed.ToString();

		public PlayerMoveState(PlayerStateMachine manager){
			this.manager = manager;
		}

		public virtual void Enter (){
			manager.anim.SetBool (animationParameters, true);
		}

		public virtual void Exit(){
			manager.anim.SetBool (animationParameters, false);

		}

		public virtual void UpdateLogic(){
			manager.anim.SetFloat (floatParameters, manager.player.MoveSpeed);
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
}
