using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine {
	public IState rollState;
	public IState jumpState;
	public IState idleState;

	public CharacterController controller;
	public PlayerController player;
	public TouchSimulation input;
	public Animator anim;

	public Vector3 velocity = Vector3.zero;

	public PlayerStateMachine(CharacterController controller, PlayerController playerController, Animator animator){
		this.controller = controller;
		player = playerController;
		anim = animator;
		rollState = new PlayerRollState (this);
		jumpState = new PlayerJumpState (this);
		idleState = new PlayerIdleState (this);
	}

	public override void Initialize (IState state)
	{
		base.Initialize (state);
		input = TouchSimulation.instance;
	}


}
