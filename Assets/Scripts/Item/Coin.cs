using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour ,IPickUpAble {
	[SerializeField] private int value = 1;
	[SerializeField] private PlayerCoin player;
	public virtual void PickUpAble(){
		player.Coin += value;
	}
}
