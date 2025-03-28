﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour {
	[SerializeField] private GameObject uiMain;

	[SerializeField] private Button goButton;
	[SerializeField] private Button systemButton;
	[SerializeField] private Text highScore;

	[SerializeField] private UISummary uiSummary;
	[SerializeField] private UISystemPlayer uiSystem;


	void Start(){
		goButton.onClick.AddListener (OnButtonGo);
		systemButton.onClick.AddListener (OnSystem);
		LoadHighScore ();
	}

	public virtual void SetActive(bool isActive){
		uiMain.SetActive (isActive);
	}

	public virtual void EndPlaying(){
		LoadHighScore ();
		ActiveSummary ();
	}

	public virtual void LoadHighScore(){
		highScore.text = GameManager.instance.HighScore.ToString ();
	}

	public virtual void ActiveSummary(){
		uiSummary.SetSummary (GameController.instance.Score.ToString(), GameController.instance.Coin.ToString());
		uiSummary.Active ();
	}

	protected virtual void OnButtonGo(){
		GameManager.instance.Play ();
		UIManager.instance.Play ();
	}

	protected virtual void OnSystem(){
		uiSystem.SetActive (true);
	}
}
