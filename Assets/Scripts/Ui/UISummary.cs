using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISummary : NddBehaviour {
	[SerializeField] protected Button exit;
	[SerializeField] protected Text score;
	[SerializeField] protected Text coin;
	[SerializeField] protected GameObject ui;
 
	void Start(){
		exit.onClick.AddListener (Exit);
		ui.SetActive (false);
	}

	public virtual void SetSummary(string textScore, string textCoin){
		score.text = textScore;
		coin.text = textCoin;
	}

	public virtual void Active(){
		ui.SetActive (true);
	}

	protected virtual void Exit(){
		ui.SetActive (false);
	}
}
