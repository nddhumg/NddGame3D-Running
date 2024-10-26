using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ItemPickUp : NddBehaviour, IPickUpAble {
	
	[SerializeField]protected EffectName effectName ;
	[SerializeField]protected PlayerEffect playerEffect;
	[SerializeField]protected ItemDestroy itemDestroy;

	void Start(){
		playerEffect = PlayerManager.instance.PlayerEffect;
	}

	public virtual void PickUpAble(GameObject obj){
		playerEffect.AddEffect (effectName);
	}

	public virtual void DestroyOnPickUp(){
		itemDestroy.Destroy ();
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadItemDestroy ();
	}

	protected virtual void LoadItemDestroy(){
		if (itemDestroy != null)
			return;
		itemDestroy = GetComponentInChildren<ItemDestroy> ();
		DebugLoadComponent ("ItemDestroy");
	}

}
