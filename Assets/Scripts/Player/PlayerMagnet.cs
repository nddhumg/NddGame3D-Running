using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
	[RequireComponent(typeof(BoxCollider))]
	public class PlayerMagnet : NddBehaviour {

		private void OnTriggerEnter(Collider collider){
			collider.GetComponent<IMagnetAble> ()?.MagnetAble();
		}
	}
}
