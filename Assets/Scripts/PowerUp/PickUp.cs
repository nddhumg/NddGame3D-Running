using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		IPickUpAble pickupAble = other.GetComponent<IPickUpAble> ();
		if (pickupAble != null){
			pickupAble.PickUpAble (transform.parent.gameObject);
			pickupAble.DestroyOnPickUp ();
		}
	}
}
