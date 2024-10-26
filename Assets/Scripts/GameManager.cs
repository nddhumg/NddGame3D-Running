using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[System.Serializable]
public struct GameData{
	public float coin;
	public float highScore;
}

public class GameManager : Singleton<GameManager> {
	[SerializeField] private float coin = 0;
	[SerializeField] private float highScore = 0;
	private State currentState = State.Idle; 

	[SerializeField] protected float maxTimeScale = 1f;


	public enum State{
		Idle,
		Hold,
		PLaying,
		Pause,
	}

	public float HighScore{
		get{ 
			return highScore;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();
		System.SaveLoadSystem.LoadGame ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			System.SaveLoadSystem.SaveGame ();
		}
	}

	private void OnApplicationQuit()
	{
		System.SaveLoadSystem.SaveGame ();
	}

	public bool IsPlay(){
		return currentState == State.PLaying;
	}

	public virtual void Save(ref GameData data){
		data.coin = coin;
		data.highScore = highScore;
	}

	public virtual void Load(GameData data){
		coin = data.coin;
		highScore = data.highScore;
	}

	public virtual void Play(){
		currentState = State.PLaying;
	}

	public virtual void Pause(){
		currentState = State.Pause;
		Time.timeScale = 0;
	}

	public virtual void Hold(){
		currentState = State.Hold;
	}

	public virtual void EndPlaying(){
		CheckHighScore ();
		currentState = State.Idle;
		coin += GameController.instance.Coin;
		UIManager.instance.EndPlaying ();
		ResetPLaying ();
	}

	public virtual void RePause(){
		Time.timeScale = 1;
		currentState = State.PLaying;
	}

	public virtual void RePause(float timeScaleStep,float waitForSeconds = 0f,float timeScaleStart = 0f){
		currentState = State.PLaying;
		StartCoroutine (CoroutineRePause (timeScaleStep,waitForSeconds,timeScaleStart));
	}

	public virtual void ResetPLaying(){
		PlayerManager.instance.ResetPlaying ();
		GameController.instance.ResetPlaying ();
		MapManager.instance.ResetPlaying ();
		ItemManager.instance.ResetPlaying ();
		UIManager.instance.ResetPlaying ();
	}

	private void CheckHighScore(){
		float score = GameController.instance.Score;
		if (score > this.highScore) {
			this.highScore = Mathf.Round (score * 100f) / 100f;
		}
	}

	private IEnumerator CoroutineRePause(float timeScaleStep,float waitForSeconds,float timeScaleStart){
		float timeScale = timeScaleStart;
		Time.timeScale = timeScale;
		yield return new WaitForSecondsRealtime (waitForSeconds);
		while (timeScale < maxTimeScale) {
			timeScale += timeScaleStep * Time.unscaledDeltaTime;
			Time.timeScale = timeScale;
			yield return null;
		}
		Time.timeScale = maxTimeScale;
	}

}
