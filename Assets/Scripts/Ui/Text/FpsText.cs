using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FpsText : TextBase {
	float fps= 0;
	protected override void Awake(){
		base.Awake ();
		InvokeRepeating ("UpdateFps", 0f, 1f);
	}

	public void UpdateFps(){
		fps = 1 / Time.deltaTime;
		fps = Mathf.Round (fps * 100f) / 100f;
		text.text ="FPS: " + fps.ToString ();
	}
}
