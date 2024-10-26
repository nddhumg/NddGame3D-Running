using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : NddBehaviour {
	[SerializeField] protected float distance = 0f;
	[SerializeField] protected float distanceDestroy = 10f;

	void Update(){
		CheckDestroyByDistance ();
	}

	public virtual void Destroy(){
		ItemManager.instance.DestroyItem (transform.parent.gameObject);
	}

	private void CheckDestroyByDistance(){
		distance = Player.PlayerManager.instance.transform.position.z - transform.position.z;
		if (distance >= distanceDestroy) {
			this.Destroy ();
		}
	}
}
