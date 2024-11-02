using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hindrance : NddBehaviour {
	[SerializeField] private Map map;

	public virtual void Destroy(){
		map.DestroyHindrace (gameObject);
		gameObject.SetActive (false);
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadScript<Map> (ref map, transform.parent.parent);
	}

}
