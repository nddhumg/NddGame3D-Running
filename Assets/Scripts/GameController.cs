using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[SerializeField] private float score = 0;
	[SerializeField] private uint multiplierScore = 1;
//	[SerializeField] private float 

	[SerializeField] private uint coin = 0;

	[SerializeField] private float timer = 0;
	[SerializeField] private float speedPlayerIncreaseDelay = 20f;
	[SerializeField] private float accelerationRate = 0.4f;
	[SerializeField] private bool upgradableSpeedPlayer = true;

	[SerializeField] private Vector3  sizeClearObstacles = new Vector3(3f,2f,2f);
	[SerializeField] private LayerMask obstacles;

	private Player.PlayerManager player;

	public uint Score{
		get{ 
			return (uint)score;
		}
	}

	public uint Coin{
		get{ 
			return coin;
		}
		set{ 
			coin = value;
		}
	}

	void Start(){
		player = Player.PlayerManager.instance;
	}

	void Update(){
		if (!GameManager.instance.IsPlay())
			return;
		score += multiplierScore * Time.deltaTime * player.PlayerController.MoveSpeed;
		GameLevel ();
	}

	public virtual void ClearHindrance(){
		RaycastHit[] hits;
		hits = Physics.BoxCastAll(player.transform.position,sizeClearObstacles, Vector3.forward, Quaternion.identity, 0,obstacles);
		foreach (RaycastHit hit in hits) {
			hit.transform.gameObject.SetActive (false);
		}
		CameraFlash.instance.Flash(1f);
	}

	public virtual void ResetPlaying(){
		coin = 0;
		score = 0;
		upgradableSpeedPlayer = true;
		timer = 0;
	}

//	void OnDrawGizmos()
//	{
//		Gizmos.color = Color.red;
//		Gizmos.DrawRay(player.transform.position,Vector3.forward * 1);
//		Gizmos.DrawWireCube(player.transform.position + Vector3.forward * 0, sizeClearObstacles*2f);
//	}


	void GameLevel(){
		if (!upgradableSpeedPlayer)
			return;
		timer += Time.deltaTime;
		if (timer < speedPlayerIncreaseDelay) {
			return;
		}
		timer = 0;
		player.PlayerController.MoveSpeed += accelerationRate;
		if (player.PlayerController.MoveSpeed == player.PlayerController.MaxSpeed)
			upgradableSpeedPlayer = false;
		
	}
		
}
