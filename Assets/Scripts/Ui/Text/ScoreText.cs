﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : TextBase {

	private string textScore = "Score: ";

	void Update(){
		text.text = textScore + GameController.instance.Score;
	}
		
}
