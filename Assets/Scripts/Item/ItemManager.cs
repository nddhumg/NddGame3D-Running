using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager> {
	[SerializeField] protected SpawnPool pool;
	[SerializeField] protected float timerSpawn = 0f;
	[SerializeField] protected float timeSpawn = 0f;
	[SerializeField] protected float minTime = 10f;
	[SerializeField] protected float maxTime = 20f;
	[SerializeField] protected bool isSpawnItem = false;
	[SerializeField] protected List<GameObject> items;
	

	public SpawnPool Pool{
		get{ 
			return pool;
		}
	}

	public bool IsSpawnItem{
		get{ 
			return isSpawnItem;
		}
	}

	void Start(){
		timeSpawn = Random.Range (minTime, maxTime);
	}

	void Update(){
		if (!GameManager.instance.IsPlay ())
			return;
		CheckSpawn ();
	}

	public virtual void ResetPlaying(){
		DestroyAllItem ();
	}

	public virtual GameObject Spawn(Vector3 position, Quaternion rotation){
		isSpawnItem = false;
		timerSpawn = 0;
		timeSpawn = Random.Range (minTime, maxTime);
		GameObject item = pool.GetFromPool (GetItemNameRandom(), position, rotation);
		items.Add (item);
		return item;
	}

	public virtual void DestroyItem(GameObject item){
		pool.ReleaseToPool (item);
		items.Remove (item);
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadPool ();
	}

	protected virtual void LoadPool(){
		if (pool != null) {
			return;
		}
		pool = GetComponent<SpawnPool> ();
		DebugLoadComponent ("SpawnPool");
	}

	protected string GetItemNameRandom(){
		return ((EffectName)Random.Range(0,System.Enum.GetValues(typeof(EffectName)).Length)).ToString();
	}

	protected virtual void DestroyAllItem(){
		pool.ReleaseToPool (items.ToArray ());
		items.Clear ();
	}

	void CheckSpawn(){
		timerSpawn += Time.deltaTime;
		if (timerSpawn >= timeSpawn && !isSpawnItem)
			isSpawnItem = true;

	}
}
