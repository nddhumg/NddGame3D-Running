using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayManager : NddBehaviour {
	[SerializeField] private GameObject uiPlay;
	[SerializeField] private UIReplay uiReplay;
	[SerializeField] private UIPause uiPause;

	[SerializeField] private Button btnPasue;

	void Start(){
		ActivesUi ();
		btnPasue.onClick.AddListener (OnPauseUI);
	}

	public virtual void SetActive(bool isActive){
		uiPlay.SetActive (isActive);
	}

	public virtual void ResetPlaying(){
		uiReplay.ResetPlaying ();
	}

	protected virtual void OnPauseUI(){
		uiPause.SetActive (true);
		GameManager.instance.Pause ();
	}

	private void ActivesUi(){
		uiReplay.SetActive (false);
		uiPause.SetActive (false);

		btnPasue.gameObject.SetActive (true);
		uiPlay.SetActive (false);
	}
}
