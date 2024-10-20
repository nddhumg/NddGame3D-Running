using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player{
	public enum AnimationParameters
	{
		isRoll,
		isJump,
		isFall,
		isMove,
		speed,
	}

	public class PlayerController : NddBehaviour{
		[SerializeField] private CharacterController controller;
		[SerializeField] private HindranceCollisionHandler hindranceCollision;
		[SerializeField] private Animator anim;

		[SerializeField] private float moveSpeed = 10f;
		[SerializeField] private float maxSpeed = 30f;
		[SerializeField] private Vector3 velocity = Vector3.zero;

		[SerializeField] private float gravity = 0.5f;
		[SerializeField] private bool isGrounded;
		[SerializeField] private Transform checkGround;
		[SerializeField] private LayerMask whatIsGround;
		[SerializeField] private float radiousCheck = 0.3f;

		[SerializeField] private bool isChangeLane = false;
		[SerializeField] private float lanePosition;
		[SerializeField] private int laneDirection;

		private PlayerStateMachine stateMachine;


		public float MoveSpeed{
			get{ 
				return moveSpeed;
			}
			set{ 
				moveSpeed = value;
				if (moveSpeed > maxSpeed) {
					moveSpeed = maxSpeed;
				}
			}
		}

		public HindranceCollisionHandler HindranceCollision{
			get{
				return hindranceCollision;
			}
		}

		public float MaxSpeed{
			get{ 
				return maxSpeed;
			}
		}

		public bool IsGrounded{
			get{ 
				return isGrounded;
			}
		}

		public void OnDead(RaycastHit hit){
			if (!GameManager.instance.IsPlay)
				return;
			GameManager.instance.IsPlay = false;
			anim.SetTrigger ("Dead");
			velocity = Vector3.zero;

		}

		protected override void LoadComponent ()
		{
			base.LoadComponent ();
			LoadCharacterController ();
			LoadHindranceCollisionHandler ();
			LoadAnimator ();
			LoadCheckGround ();
		}

		protected virtual void LoadCharacterController(){
			if (controller != null)
				return;
			controller = GetComponent<CharacterController> ();
			DebugLoadComponent ("CharacterController");
		}

		protected virtual void LoadHindranceCollisionHandler(){
			if (hindranceCollision != null)
				return;
			hindranceCollision = GetComponentInChildren<HindranceCollisionHandler> ();
			DebugLoadComponent ("HindranceCollisionHandler");
		}

		protected virtual void LoadCheckGround(){
			if (checkGround != null)
				return;
			checkGround = transform.Find("checkGround");
			DebugLoadComponent ("CheckGround");
		}

		protected virtual void LoadAnimator(){
			if (anim != null)
				return;
			anim = GetComponentInChildren<Animator> ();
			DebugLoadComponent ("Animator");
		}

		void Start(){
			stateMachine = new PlayerStateMachine (controller, this, anim);
			stateMachine.Initialize (stateMachine.idleState);
			hindranceCollision.OnCollision += OnDead;
		}

		void FixedUpdate(){
			stateMachine.FixedUpdate ();
			velocity += stateMachine.velocity;
			HandleVelocity ();
			HandleAnimation ();
			controller.Move (velocity);
		}

		void Update(){
			stateMachine.Update ();
			CheckChangeLane ();
			CheckGrounded ();
		}

		void HandleVelocity(){
			Falling ();
			if (!GameManager.instance.IsPlay)
				return;
			ChangeLane ();
			Move ();
		}

		void HandleAnimation(){
			if (!GameManager.instance.IsPlay)
				return;
			if (velocity.y < 0)
				anim.SetBool ("isFall", true);
			if(isGrounded) {
				anim.SetBool ("isFall", false);
			}
		}

		void CheckChangeLane(){
			if (TouchSimulation.instance.LeftTouch) {
				ChangeLaneEnter (Swipe.Left);
			}
			else if (TouchSimulation.instance.RightTouch) {
				ChangeLaneEnter (Swipe.Right);
			}
		}


		void CheckGrounded(){
			isGrounded = Physics.CheckSphere(checkGround.position,radiousCheck,whatIsGround);
			if (isGrounded && velocity.y < 0) {
				velocity.y = 0;
			}
		}

//		void OnDrawGizmosSelected()
//		{
//			Gizmos.color = Color.yellow;
//			Gizmos.DrawSphere(checkGround.position, radiousCheck);
//		}

		void ChangeLane(){
			if (!isChangeLane)
				return;
			velocity.x = laneDirection * Time.fixedDeltaTime * moveSpeed;
			if (IsFinishChangeLane()) {
				isChangeLane = false;
				velocity.x = 0;
				laneDirection = 0;
			}

		}

		bool IsFinishChangeLane(){
			float positionPlayer = laneDirection * transform.position.x;
			float positionLane = laneDirection * lanePosition;
			if (positionPlayer < positionLane) {
				return false;
			} 
			else {
				return true;
			}
		}

		void ChangeLaneEnter(Swipe swipe){
			if (!LaneManager.instance.CanChange (swipe))
				return;
			LaneManager.instance.ChangeLane (swipe);
			lanePosition = LaneManager.instance.GetLanePosition ();
			isChangeLane = true;
			laneDirection = lanePosition - transform.position.x < 0 ? -1 : 1;
		}

		void Move(){
			velocity.z = transform.forward.z * moveSpeed * Time.fixedDeltaTime;
		}

		void Falling(){
			if (isGrounded)
				return;
			velocity.y -= gravity * Time.fixedDeltaTime;
		}

		bool IsPlay(){
			if (GameManager.instance.IsPlay) {
				return true;
			} else {
				velocity = Vector3.zero;
				return false;
			}
		}

	}
}