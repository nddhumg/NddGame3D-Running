using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
public enum AnimationParameters
{
	isRoll,
	isJump,
	isFall,
	isMove,
	speed,
}

public class PlayerController : MonoBehaviour {
	[SerializeField] private CharacterController controller;
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
	void Start(){
		stateMachine = new PlayerStateMachine (controller, this, anim);
		stateMachine.Initialize (stateMachine.idleState);
	}

	void FixedUpdate(){
		stateMachine.FixedUpdate ();
		HandleVelocity ();
		velocity += stateMachine.velocity;
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
		Grounded ();
		ChangeLane ();
		Move ();
	}

	void HandleAnimation(){
		if (velocity.y < 0)
			anim.SetBool ("isFall", true);
		if(isGrounded) {
			anim.SetBool ("isFall", false);
		}
	}

	void CheckChangeLane(){
		if (TouchSimulation.instance.LeftTouch) {
			ChangeLaneEnter (GameController.Swipe.Left);
		}
		else if (TouchSimulation.instance.RightTouch) {
			ChangeLaneEnter (GameController.Swipe.Right);
		}
	}


	void CheckGrounded(){
		isGrounded = Physics.CheckSphere(checkGround.position,radiousCheck,whatIsGround);
		if (isGrounded && velocity.y < 0) {
			velocity.y = 0;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(checkGround.position, radiousCheck);
	}

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
		if (laneDirection * transform.position.x >= laneDirection * lanePosition)
			return true;
		else
			return false;
	}

	void ChangeLaneEnter(GameController.Swipe swipe){
		if (!GameController.instance.CanChange (swipe))
			return;
		GameController.instance.ChangeLane (swipe);
		lanePosition = GameController.instance.GetLanePosition ();
		isChangeLane = true;
		laneDirection = lanePosition - transform.position.x < 0 ? -1 : 1;
	}

	void Move(){
		velocity.z = transform.forward.z * moveSpeed * Time.fixedDeltaTime;
	}

	void Grounded(){
		if (isGrounded && velocity.y < 0) {
			velocity.y = 0;
		}
	}

	void Falling(){
		if (isGrounded)
			return;
		velocity.y -= gravity * Time.fixedDeltaTime;
	}

}
}