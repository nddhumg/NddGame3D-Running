using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	[SerializeField] private bool isPlay = true;

	public bool IsPlay{
		get{ 
			return isPlay;
		}
		set{ 
			isPlay = value;
		}
	}

	public virtual void Play(){
		isPlay = true;
	}
}
