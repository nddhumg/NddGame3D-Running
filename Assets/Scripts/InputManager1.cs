using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager1 : MonoBehaviour {
	public bool left;
	public bool right;
	public bool up;
	public bool down;

	void Update(){
		if (Input.touchCount > 0) {
			Debug.Log (">0");
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Moved)
				Debug.Log ("touch move");
			
			if (Input.touchCount == 2) {
				touch = Input.GetTouch (1);

				if (touch.phase == TouchPhase.Began)
					Debug.Log ("touch Began");
				if (touch.phase == TouchPhase.Ended)
					Debug.Log ("touch Ended");
			}
		}
	}
}

