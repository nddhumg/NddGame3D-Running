using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : NddBehaviour {
	[SerializeField] private float lenght = 0;
	[SerializeField] private float width = 9f;

	[SerializeField] private Transform[] transformItem;

	public float Lenght{
		get
		{ 
			return lenght;
		}
	}

	public Transform[] TransformItem{
		get
		{ 
			return transformItem;
		}
	}

	protected override void Reset ()
	{
		base.Reset ();
		LoadMap ();
	}

	public Vector3 GetRandomPositionItem(){
		int random = Random.Range (0, transformItem.Length);
		return transformItem [random].position;
	}



	private void LoadMap(){
		ChangeScaleGroundToLenght ();
	}

	private void ChangeScaleGroundToLenght(){
		Transform ground = transform.Find ("Ground");
		float sizeLast = 0f;
		float sizeCurrent = 0f;
		Vector3 position = Vector3.zero;
		foreach (Transform tf in ground) {
			MeshRenderer mesh = tf.GetComponent<MeshRenderer> ();
			tf.localPosition = position;
			sizeCurrent = mesh.bounds.size.z;
			sizeLast = sizeCurrent;
			position.z += sizeCurrent/2 + sizeLast/2;
			lenght += sizeCurrent;
		}
		BoxCollider col = transform.GetComponent<BoxCollider>();
		col.size = new Vector3(width,0.5f,lenght);
		Vector3 center = Vector3.zero;
		center.y = -0.23f;
		center.z += lenght / 2 + transform.position.z;
		col.center = center;
	}
}
