using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : Singleton<LaneManager> {
	[SerializeField] private float[] lanes = {-2.8f ,0, 2.8f};
	[SerializeField] private int currentLane = 1;

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
