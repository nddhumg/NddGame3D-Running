using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour {
	[SerializeField] private int coin;

	public int Coin{
		get{ 
			return coin;
		}
		set{ 
			coin = value;
		}
	}
}
