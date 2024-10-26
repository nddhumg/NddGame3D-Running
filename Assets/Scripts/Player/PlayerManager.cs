using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
	public class PlayerManager : Singleton<PlayerManager> {
		[SerializeField] private HindranceCollisionHandler collision;
		[SerializeField] private PlayerController playerController;
		[SerializeField] private PlayerEffect playerEffect;

		public PlayerEffect PlayerEffect{
			get{ 
				return playerEffect;
			}
		}

		public HindranceCollisionHandler Collision{
			get{ 
				return collision;
			}
		}

		public PlayerController PlayerController{
			get{ 
				return playerController;
			}
		}

//		public stat

		public virtual void ResetPlaying(){
			playerController.ResetPlaying ();
			playerEffect.OffAllEffect ();
			playerController.ReplayAnimation ();
		}

		protected override void LoadComponent ()
		{
			base.LoadComponent ();
			LoadHindranceCollisionHandler();
			LoadPlayerController();
			LoadPLayerEffect ();
		}

		protected virtual void LoadHindranceCollisionHandler(){
			if (collision != null)
				return;
			collision = GetComponentInChildren<HindranceCollisionHandler> ();
			DebugLoadComponent ("HindranceCollisionHandler");
		}

		protected virtual void LoadPLayerEffect(){
			if (playerEffect != null)
				return;
			playerEffect = GetComponentInChildren<PlayerEffect> ();
			DebugLoadComponent ("PlayerEffect");
		}

		protected virtual void LoadPlayerController(){
			if (playerController != null)
				return;
			playerController = GetComponent<PlayerController> ();
			DebugLoadComponent ("PlayerController");
		}
	}
}
