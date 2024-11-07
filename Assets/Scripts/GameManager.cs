using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[System.Serializable]
public struct GameData{
	public uint coin;
	public uint highScore;
}

public class GameManager : Singleton<GameManager> {
	[SerializeField] private uint coin = 0;
	[SerializeField] private uint highScore = 0;
	private State currentState = State.Idle; 

	[SerializeField] protected float maxTimeScale = 1f;
	private Coroutine rePause;


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

	protected virtual void OnApplicationQuit()
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

	public virtual void RePlay(){
		currentState = State.PLaying;
		PlayerManager.instance.PlayerController.ReplayAnimation ();
		GameController.instance.ClearHindrance ();
	}

	public virtual void Pause(){
		if(rePause != null)
			StopCoroutine (rePause);
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
		rePause = StartCoroutine (CoroutineRePause (timeScaleStep,waitForSeconds,timeScaleStart));
	}

	public virtual void ResetPLaying(){
		PlayerManager.instance.ResetPlaying ();
		GameController.instance.ResetPlaying ();
		MapManager.instance.ResetPlaying ();
		ItemManager.instance.ResetPlaying ();
		UIManager.instance.ResetPlaying ();
	}

	private void CheckHighScore(){
		if (GameController.instance.Score > this.highScore) {
			this.highScore = GameController.instance.Score;
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
