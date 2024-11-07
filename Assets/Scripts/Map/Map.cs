using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : NddBehaviour {
	[SerializeField] private float lenght = 0;
	[SerializeField] private float width = 9f;
	[SerializeField] private List<GameObject> hindranceDestroy;
	[SerializeField] private List<Transform> transformItem = new List<Transform>();

	void OnEnable(){
		HindraceActive ();
	}

	public float Lenght{
		get
		{ 
			return lenght;
		}
	}

	public List<Transform> TransformItem{
		get
		{ 
			return transformItem;
		}
	}
	public bool IsMapHasItem(){
		return transformItem.Count != 0;  
	}

	public Vector3 GetRandomPositionItem(){
		int random = Random.Range (0, transformItem.Count);
		return transformItem [random].position;
	}


	public void DestroyHindrace(GameObject hindrace){
		hindranceDestroy.Add(hindrace);
	}

	private void LoadMap(){
		ChangeScaleGroundToLenght ();
	}

	protected void HindraceActive(){
		if (hindranceDestroy.Count == 0)
			return;
		foreach (GameObject obj in hindranceDestroy) {
			obj.SetActive (true);
		}
		hindranceDestroy.Clear ();
	}

	protected override void Reset ()
	{
		base.Reset ();
		LoadMap ();
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadPositionItem ();
	}

	protected virtual void LoadPositionItem(){
		if (this.transformItem.Count > 0) {
			return;
		}
		Transform parent = transform.Find ("PositionItem");
		if (parent == null)
			return;
		foreach (Transform child in parent) {
			this.transformItem.Add (child);
		}
		DebugLoadComponent ("PositionItem");
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
