using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player{
	public class PlayerRollState : IState {
		private float timeRoll = 0.7f;
		private float rollHeight = 1.3f;
		private float height = 2.5f;
		private float timer = 0;
		private Vector3 centerRoll = new Vector3 (0, -0.5f, 0);
		private Vector3 center = new Vector3 (0, 0.15f, 0);

		string animationParameters = AnimationParameters.isRoll.ToString();

		private PlayerStateMachine manager;

		public PlayerRollState(PlayerStateMachine manager){
			this.manager = manager;
		}

		public virtual void Enter (){
			manager.anim.SetBool (animationParameters, true);
			if (!manager.player.IsGrounded)
				manager.velocity.y = -0.5f;
			manager.player.HindranceCollision.SetSizeBox (true);
			manager.controller.height = rollHeight;
			manager.controller.center = centerRoll;
		}

		public virtual void Exit(){
			timer = 0;
			manager.player.HindranceCollision.SetSizeBox (false);
			manager.controller.center = center;
			manager.velocity.y = 0;
			manager.controller.height = height;
			manager.anim.SetBool (animationParameters, false);
		}

		public virtual void UpdateLogic(){
			if (manager.input.UpTouch) {
				manager.ChangeState (manager.jumpState);
			}
			if (manager.player.IsGrounded) {
				timer += Time.deltaTime;
			}
			if (timer >= timeRoll) {
				manager.ChangeState (manager.idleState);
			}
		}

		public virtual void UpdatePhysics(){
		}
	}
}
