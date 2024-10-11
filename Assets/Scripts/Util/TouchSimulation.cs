using UnityEngine;

public class TouchSimulation : Singleton<TouchSimulation>
{
	private Vector2 simulatedTouchPosition;

	[SerializeField]private bool left;
	[SerializeField]private bool right;
	[SerializeField]private bool up;
	[SerializeField]private bool down;

	[SerializeField]private Vector3 touchPositionStart ;
	[SerializeField]private Vector3 touchPositionEnd;
	[SerializeField]private float angle;
	[SerializeField]private float swipeDistance ;
	[SerializeField]private float threshold = 50f;
	private bool isStart = false;
	#if UNITY_EDITOR
	private Vector2 lastPositionMouse;
	#endif
	public bool LeftTouch{
		get{ 
			return left;
		}
	}

	public bool UpTouch{
		get{ 
			return up;
		}
	}

	public bool DownTouch{
		get{ 
			return down;
		}
	}

	public bool RightTouch{
		get{ 
			return right;
		}
	}

	void Update()
	{
		left = false;
		right = false;
		up = false;
		down = false;


		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			HandleTouch(touch.position, touch.phase);
		}

		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) // Chuột trái được nhấn
		{
			simulatedTouchPosition = Input.mousePosition;
			HandleTouch(simulatedTouchPosition, TouchPhase.Began);
		}
		else if (Input.GetMouseButton(0)) // Chuột trái đang giữ
		{
			simulatedTouchPosition = Input.mousePosition;
			if(lastPositionMouse == simulatedTouchPosition){
				HandleTouch(simulatedTouchPosition, TouchPhase.Stationary);
			}
			lastPositionMouse = simulatedTouchPosition;
		}
		else if (Input.GetMouseButtonUp(0)) // Chuột trái được thả
		{
			simulatedTouchPosition = Input.mousePosition;
			HandleTouch(simulatedTouchPosition, TouchPhase.Ended);
		}
		#endif
	}

	// Hàm xử lý thao tác touch hoặc giả lập từ chuột
	private void HandleTouch(Vector2 touchPosition, TouchPhase phase)
	{
		switch (phase)
		{
		case TouchPhase.Began:
			touchPositionStart = touchPosition;
			touchPositionEnd = touchPosition;
			break;
		case TouchPhase.Stationary:
			if (!isStart) {
				touchPositionStart = touchPosition;
				isStart = true;
			}
			touchPositionEnd = touchPosition;
			break;
		case TouchPhase.Ended:
			touchPositionEnd = touchPosition;
			break;
		}

		if (IsSwipeThresholdMet ()) {
			CheckSwipeDirection();
			isStart = false;
		}
	}


	private bool IsSwipeThresholdMet(){
		swipeDistance = Vector3.Distance (touchPositionStart, touchPositionEnd);
		return swipeDistance >= threshold;
	}

	private void CheckSwipeDirection(){
		Vector3 vectoTouch = touchPositionStart - touchPositionEnd;
		angle = Vector3.Angle (vectoTouch,Vector3.right);
		angle = Vector3.Angle (Vector3.down, vectoTouch) > 90 ? -angle :angle;

		if (Mathf.Abs (angle) < 30) {
			left = true;
		} else if (Mathf.Abs (angle) > 140) {
			right = true;
		} else if (60 < angle && angle < 110) {
			up = true;
		} else if (-60 > angle && angle > -140) {
			down = true;
		}
	}
}
