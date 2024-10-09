using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[SerializeField] private float[] lanes;
	[SerializeField] private int currentLane = 1;

	public enum Swipe{
		Left,
		Right,
		Down,
		Up
	}


	public void ChangeLane(Swipe swipe){
		if (CanChange(swipe) && swipe == Swipe.Left ) {
			currentLane -= 1;
		} 
		else if (CanChange(swipe) && swipe == Swipe.Right ) {
			currentLane += 1;
		} 
		else {
//			Debug.LogWarning ();
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
