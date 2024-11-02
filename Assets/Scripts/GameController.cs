using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	[SerializeField] private Player.PlayerController player;
	[SerializeField] private float score = 0;
	[SerializeField] private float scorePerSecond = 0.25f;
//	[SerializeField] private float 

	[SerializeField] private float coin = 0;

	[SerializeField] private float timer = 0;
	[SerializeField] private float speedPlayerIncreaseDelay = 20f;
	[SerializeField] private float accelerationRate = 0.4f;
	[SerializeField] private bool upgradableSpeedPlayer = true;

	[SerializeField] private Vector3  sizeClearObstacles = new Vector3(0f,0f,0f);
	[SerializeField] private LayerMask obstacles;

	public float Score{
		get{ 
			return score;
		}
	}

	public float Coin{
		get{ 
			return coin;
		}
		set{ 
			coin = value;
		}
	}

	void Update(){
		if (!GameManager.instance.IsPlay())
			return;
		score += scorePerSecond * Time.deltaTime * player.MoveSpeed;
		GameLevel ();
	}

	public virtual void ClearHindrance(){
		RaycastHit[] hits;
		hits = Physics.BoxCastAll(player.transform.position,sizeClearObstacles, Vector3.forward, Quaternion.identity, 0,obstacles);
		Hindrance hindrance ;
		foreach (RaycastHit hit in hits) {
			hindrance = hit.transform.GetComponent<Hindrance> ();
			hindrance.Destroy();
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
		player.MoveSpeed += accelerationRate;
		if (player.MoveSpeed == player.MaxSpeed)
			upgradableSpeedPlayer = false;
		
	}
		
}
