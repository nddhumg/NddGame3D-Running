using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour {
	[SerializeField] protected GameObject ui;
	[SerializeField] private Button buttonContinue;
	[SerializeField] private Button buttonGiveUp;

	[SerializeField] private Button btnExit;
	[SerializeField] private Button btnExitBG;

	void Start(){
		buttonContinue.onClick.AddListener (OnContinue );
		buttonGiveUp.onClick.AddListener (OnGiveUp );
		btnExit.onClick.AddListener (ExitUI);
		btnExitBG.onClick.AddListener (ExitUI);
	}

	public virtual void SetActive(bool isActive){
		ui.SetActive (isActive);
		btnExitBG.gameObject.SetActive (isActive);

	}

	protected virtual void OnContinue(){
		ExitUI ();
	}

	protected virtual void OnGiveUp(){
		SetActive (false);
		UIManager.instance.EndPlaying ();
		GameManager.instance.EndPlaying ();
		Time.timeScale = 1;
	}

	protected virtual void ExitUI(){
		SetActive (false);
		GameManager.instance.RePause (1f, 0.5f, 0.4f);
	} 
}
