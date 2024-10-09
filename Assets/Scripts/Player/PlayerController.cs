using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pc;

public class PlayerController : MonoBehaviour {
	[SerializeField] private CharacterController controller;
	[SerializeField] private float height = 2f;

	[SerializeField] private float speed = 10f;
	[SerializeField] private Vector3 velocity = Vector3.zero;

	[SerializeField] private float gravity = 0.5f;
	[SerializeField] private bool isGrounded;
	[SerializeField] private Transform checkGround;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private float radiousCheck = 0.2f;

	[SerializeField] private bool isChangeLane = false;
	[SerializeField] private float lanePosition;
	[SerializeField] private float laneChangeSpeed = 10;
	[SerializeField] private int laneDirection;

	[SerializeField] private float timeRoll = 0.2f;
	[SerializeField] private float rollHeight = 0.7f;
	[SerializeField] private bool isRoll = false;



	void FixedUpdate(){
		HandleVelocity ();
		HandleAnimation ();
		controller.Move (velocity);
	}

	void HandleVelocity(){
		CheckGrounded ();
		CheckChangeLane ();
		Move ();
		Jumb ();
		CheckRoll ();
		Falling ();
	}

	void HandleAnimation(){
	}

	void CheckChangeLane(){
		if (InputManager.instance.left) {
			ChangeLane (GameController.Swipe.Left);
		}
		if (InputManager.instance.right) {
			ChangeLane (GameController.Swipe.Right);
		}

		if (isChangeLane) {
			velocity.x = laneDirection * Time.fixedDeltaTime * laneChangeSpeed;
			if (IsFinishChangeLane()) {
				isChangeLane = false;
				velocity.x = 0;
				laneDirection = 0;
				return;
			}
		}
	}

	bool IsFinishChangeLane(){
		if (laneDirection * transform.position.x >= laneDirection * lanePosition)
			return true;
		else
			return false;
	}

	void ChangeLane(GameController.Swipe swipe){
		if (!GameController.instance.CanChange (swipe))
			return;
		GameController.instance.ChangeLane (swipe);
		lanePosition = GameController.instance.GetLanePosition ();
		isChangeLane = true;
		laneDirection = lanePosition - transform.position.x < 0 ? -1 : 1;
	}

	void Move(){
		velocity.z = transform.forward.z * speed * Time.fixedDeltaTime;
	}

	void Jumb(){
		if(InputManager.instance.up && isGrounded){
			velocity.y = 0.2f;
		}
	}

	void CheckGrounded(){
		isGrounded = Physics.CheckSphere(checkGround.position,radiousCheck,whatIsGround);
		if (isGrounded && velocity.y < 0) {
			velocity.y = 0;
		}
	}

	void CheckRoll(){
		if (InputManager.instance.down && !isRoll) {
			StartCoroutine (Roll ());
		}
	}

	IEnumerator Roll(){
		isRoll = true;
		if (isGrounded) {
			controller.height = rollHeight;
		} else {
			controller.height = rollHeight;
			velocity.y = -0.5f	;
		}
		yield return new WaitForSeconds (timeRoll);
		controller.height = height; 
		isRoll = false;
	}

	void Falling(){
		if (isGrounded)
			return;
		velocity.y -= gravity * Time.fixedDeltaTime;
	}
}
