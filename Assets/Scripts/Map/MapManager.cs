using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapName{
	Map,
}
public class MapManager : Singleton<MapManager> {
	[SerializeField] private SpawnPool pool;
	 private Queue<GameObject> maps = new Queue<GameObject>();
	[SerializeField] private Vector3 startPositionSpawnNext = new Vector3(0,-1.2f,0);

	[SerializeField] private Transform player;
	[SerializeField] private float spawnDistacne = 100f;

	[SerializeField] private Transform lastMap ;
	[SerializeField] private Vector3 endPositionLastMap;
	[SerializeField] private float distanceDestroyMap = 20f;

	void Start(){
		Spawn ();
		CaculatoLastMapNew();
	}

	void Update(){
		if (IsPlayerNearMapEnd()) {
			Spawn ();
		}
		if (IsPlayerPassedLastMap ()) {
			DestroyMap ();
		}
	}

	private bool IsPlayerNearMapEnd()
	{
		float distance = startPositionSpawnNext.z - player.position.z;
		if (distance <= spawnDistacne)
			return true;
		return false;
	}

	void Spawn(){
		GameObject map = pool.GetFromPool (GetMapNameSpawn(), Vector3.zero, Quaternion.identity);
		float mapLenght = map.GetComponent<Map> ().Lenght;
		map.transform.position = startPositionSpawnNext;
		startPositionSpawnNext.z += mapLenght;
		maps.Enqueue (map);
	}

	void DestroyMap(){
		pool.ReleaseToPool (lastMap.gameObject);
		CaculatoLastMapNew ();
	}

	void CaculatoLastMapNew(){
		lastMap = maps.Dequeue ().transform;
		endPositionLastMap = GetEndPositionLastMap ();
	}

	bool IsPlayerPassedLastMap(){
		if (player.position.z - endPositionLastMap.z >= distanceDestroyMap) {
			return true;
		}
		return false;
	}

	Vector3 GetEndPositionLastMap(){
		Vector3 endPosition;
		float mapLenght = lastMap.GetComponent<Map> ().Lenght;
		endPosition = lastMap.position;
		endPosition.z += mapLenght ;
		return endPosition;
	}
		
	string GetMapNameSpawn(){
		return MapName.Map.ToString ();
	}


}
 