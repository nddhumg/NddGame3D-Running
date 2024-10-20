using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PowerUpPickUp : NddBehaviour, IPickUpAble {
	
	[SerializeField]protected EffectName effectName ;
	[SerializeField]protected PlayerEffect playerEffect;

	void Start(){
		playerEffect = PlayerManager.instance.PlayerEffect;
	}

	public virtual void PickUpAble(GameObject obj){
		playerEffect.AddEffect (effectName);
	}

	public virtual void DestroyOnPickUp(){
		gameObject.SetActive (false);
	}

}
