using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReplay : NddBehaviour {
	[SerializeField] private Button replayButton;
	[SerializeField] private Button exitReplayButton;

	[SerializeField] private Slider sliderExit;
	[SerializeField] private float elapsedExit = 0f;
	[SerializeField] private float durationExit = 5f;

	[SerializeField] private GameObject ui;

	[SerializeField] private Coroutine sliderCoroutine;
	private bool isActionReplay = false;

	void Start(){
		AddActionReplay ();
		SetActive (false);
		replayButton.onClick.AddListener (Replay);
		exitReplayButton.onClick.AddListener (ExitReplay);
	}

	public virtual void Replay(){
		StopCoroutine(sliderCoroutine);
		GameManager.instance.RePlay ();
		SetActive (false);
		isActionReplay = true;
		AddActionReplay ();

	}

	public virtual void ResetPlaying(){
		if (isActionReplay) {
			Player.PlayerManager.instance.PlayerController.OnDead -= ExitReplay;
		}
		isActionReplay = false;
		AddActionReplay ();
	}

	public virtual void ExitReplay(){
		SetActive (false);
		UIManager.instance.EndPlaying();
		GameManager.instance.EndPlaying ();
	}

	public void SetActive(bool isActive){
		ui.SetActive (isActive);
	}
		
	protected virtual void OnUIReplay(){
		StartCoroutine (Active());
	}

	IEnumerator Active(){
		yield return new WaitForSeconds (1f);

		SetActive (true);
		sliderCoroutine = StartCoroutine (SliderOverTime());
	}

	IEnumerator SliderOverTime(){
		elapsedExit = 0;
		while (elapsedExit < durationExit) {
			elapsedExit += Time.deltaTime;
			sliderExit.value = elapsedExit / durationExit;
			yield return null;
		}
		ExitReplay ();
	}

	private void AddActionReplay(){
		if (isActionReplay) {
			Player.PlayerManager.instance.PlayerController.OnDead -= OnUIReplay;
			Player.PlayerManager.instance.PlayerController.OnDead += ExitReplay;
		} else {
			Player.PlayerManager.instance.PlayerController.OnDead += OnUIReplay;
		}
	}

}
