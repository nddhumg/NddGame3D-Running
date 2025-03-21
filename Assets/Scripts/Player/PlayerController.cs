﻿using System.Collections;
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

		public Action OnDead;

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

		public void Dead(){
			if (!GameManager.instance.IsPlay())
				return;
			GameManager.instance.Hold ();
			stateMachine.ChangeState (stateMachine.deadState);
			velocity = Vector3.zero;
			CameraShake.instance.Shake (0.25f,  0.2f);
			OnDead?.Invoke ();
		}

		public void ReplayAnimation(){
			stateMachine.ChangeState (stateMachine.idleState);
		}

		public virtual void ResetPlaying(){
			moveSpeed = 10f;
			transform.position = Vector3.zero;
			velocity = Vector3.zero;
		}

		protected override void LoadComponent ()
		{
			base.LoadComponent ();
			LoadScript<CharacterController> (ref controller);
			LoadScriptInChild<HindranceCollisionHandler> (ref hindranceCollision);
			LoadScript<Animator> (ref anim);
			LoadCheckGround ();
		}

		protected virtual void LoadCheckGround(){
			if (checkGround != null)
				return;
			checkGround = transform.Find("checkGround");
			DebugLoadComponent ("CheckGround");
		}

		void Start(){
			stateMachine = new PlayerStateMachine (controller, this, anim);
			stateMachine.Initialize (stateMachine.idleState);
			hindranceCollision.OnCollision += Dead;
		}

		void FixedUpdate(){
			HandleVelocity ();
			HandleAnimation ();
			controller.Move (velocity);
		}

		void Update(){
			if (!GameManager.instance.IsPlay ())
				return;
			stateMachine.Update ();
			CheckChangeLane ();
			CheckGrounded ();
		}

		void HandleVelocity(){
			Falling ();
			if (!GameManager.instance.IsPlay())
				return;
			stateMachine.FixedUpdate ();
			velocity += stateMachine.velocity;
			ChangeLane ();
			Move ();
		}

		void HandleAnimation(){
			if (!GameManager.instance.IsPlay())
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

	}
}