using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateCircularRotation : MonoBehaviour {
	[SerializeField] private float duration = 10f;
	[SerializeField] private Vector3 endRotation = Vector3.zero;
	[SerializeField] private bool isLoop = true;
	[SerializeField] private bool isKill = false;
	Tween tween;



	void OnEnable(){
		if (tween == null)
			CircularRotation ();
		else
			tween.Play ();
	}

	void OnDisable(){
		if (isKill) {
			tween.Kill ();
		} else {
			tween.Pause ();
		}
	}

	private void CircularRotation(){
		tween = transform.DORotate (endRotation, duration, RotateMode.FastBeyond360).SetEase (Ease.Linear);
		if (isLoop)
			tween.SetLoops (-1, LoopType.Restart);
	}


}
