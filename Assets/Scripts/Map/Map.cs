using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	[SerializeField] private float lenght;

	public float Lenght{
		get
		{ 
			return lenght;
		}
	}


	void Reset(){
		LoadMap ();
	}

	private void LoadMap(){
		ChangeScaleGroundToLenght ();
	}

	private void ChangeScaleGroundToLenght(){
		Transform ground = transform.Find ("Ground");
		float scaleToPostionRatio = 10f;
		lenght = ground.lossyScale.z * scaleToPostionRatio;
	}
}
