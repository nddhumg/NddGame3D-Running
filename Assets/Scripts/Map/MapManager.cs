using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapName{
	Map,
}
public class MapManager : Singleton<MapManager> {
	[SerializeField] private SpawnPool pool;
//	[SerializeField] private Queue<GameObject> maps = new Queue<GameObject>();
	[SerializeField] private Vector3 positonStartSpawnNext = new Vector3(0,-1.2f,0);

	[SerializeField] private Transform player;
	[SerializeField] private float spawnDistacne = 100f;


	void Start(){
		Spawn ();
	}

	void Update(){
		if (IsPlayerNearMapEnd()) {
			Spawn ();
		}
	}

	private bool IsPlayerNearMapEnd()
	{
		float distance = positonStartSpawnNext.z - player.position.z;
		if (distance <= spawnDistacne)
			return true;
		return false;
	}

	void Spawn(){
		GameObject map = pool.GetFromPool (GetMapNameSpawn(), Vector3.zero, Quaternion.identity);
		float mapLenght = map.GetComponent<Map> ().Lenght;
		Vector3 positionSpawn = GetPositionFromMapPositionStart(positonStartSpawnNext,mapLenght);
		map.transform.position = positionSpawn;
		positonStartSpawnNext.z += mapLenght;
	}

	void DestroyMap(GameObject map){
		pool.ReleaseToPool (map);
	}
		
	string GetMapNameSpawn(){
		return MapName.Map.ToString ();
	}

	private Vector3 GetPositionFromMapPositionStart(Vector3 startPosition, float lenght){
		Vector3 positionMid = startPosition;
		positionMid.z += lenght/2f;
		return positionMid;
	}

}
 