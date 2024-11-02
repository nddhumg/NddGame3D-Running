using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
	public class PlayerDeadState : IState {
		private PlayerStateMachine manager;

		public PlayerDeadState(PlayerStateMachine manager){
			this.manager = manager;
		}

		public virtual void Enter (){
			manager.anim.SetTrigger ("Dead");
			manager.velocity = Vector3.zero;
		}

		public virtual void Exit(){
			manager.anim.SetTrigger("Replay");
		}

		public virtual void UpdateLogic(){
		}

		public virtual void UpdatePhysics(){

		}
	}
}
