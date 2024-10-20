using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[SerializeField] private Player.PlayerController player;
	[SerializeField] private float score = 0;
	[SerializeField] private float scorePerSecond = 1;

	[SerializeField] private float coin = 0;

	[SerializeField] private float timer = 0;
	[SerializeField] private float speedPlayerIncreaseDelay = 20f;
	[SerializeField] private float accelerationRate = 0.4f;
	[SerializeField] private bool upgradableSpeedPlayer = true;


	public float Score{
		get{ 
			return score;
		}
	}

	public float Coin{
		get{ 
			return coin;
		}
		set{ 
			coin = value;
		}
	}

	void Update(){
		if (!GameManager.instance.IsPlay)
			return;
		score += scorePerSecond * Time.deltaTime * player.MoveSpeed;
		GameLevel ();
	}


	void GameLevel(){
		if (!upgradableSpeedPlayer)
			return;
		timer += Time.deltaTime;
		if (timer < speedPlayerIncreaseDelay) {
			return;
		}
		timer = 0;
		player.MoveSpeed += accelerationRate;
		if (player.MoveSpeed == player.MaxSpeed)
			upgradableSpeedPlayer = false;
		
	}
		
}
