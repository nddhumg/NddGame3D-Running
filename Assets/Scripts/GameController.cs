using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[SerializeField] private Player.PlayerController player;
	[SerializeField] private float[] lanes;
	[SerializeField] private int currentLane = 1;
	[SerializeField] private float score = 0;
	[SerializeField] private float scorePerSecond = 1;

	[SerializeField] private float timer = 0;
	[SerializeField] private float speedPlayerIncreaseDelay = 20f;
	[SerializeField] private float accelerationRate = 0.4f;
	[SerializeField] private bool upgradableSpeedPlayer = true;

	public enum Swipe{
		Left,
		Right,
		Down,
		Up
	}

	void Update(){
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

	public void ChangeLane(Swipe swipe){
		if (CanChange(swipe) && swipe == Swipe.Left ) {
			currentLane -= 1;
		} 
		else if (CanChange(swipe) && swipe == Swipe.Right ) {
			currentLane += 1;
		} 
		else {
			Debug.LogWarning ("Dont ChangeLane by swipe " + swipe.ToString());
		}
	}

	public float GetLanePosition(){
		return lanes [currentLane];
	}

	public bool CanChange(Swipe swipe){
		if (swipe == Swipe.Left && currentLane -1 >= 0 ) {
			return true;
		} 
		else if (swipe == Swipe.Right && currentLane + 1 <= 2) {
			return true;
		} 
		return false;
	}
}
