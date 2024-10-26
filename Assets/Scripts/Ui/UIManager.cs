using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	[SerializeField] private UIMainManager uiMain;
	[SerializeField] private UIPlayManager uiPlay;

	void Start(){
		uiMain.SetActive (true);
		uiPlay.SetActive (false);

	}

	public virtual void Play(){
		uiMain.SetActive (false);
		uiPlay.SetActive (true);


	}

	public virtual void EndPlaying(){
		uiMain.SetActive (true);
		uiPlay.SetActive (false);

		uiMain.EndPlaying ();
	}

	public virtual void ResetPlaying(){
		uiPlay.ResetPlaying ();
	}
}
