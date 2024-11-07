using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hindrance : NddBehaviour {
	[SerializeField] protected Map map;

	protected virtual void OnDisable(){
		Destroy ();
	}

	protected virtual void Destroy(){
		map.DestroyHindrace (gameObject);
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadScript<Map> (ref map, transform.parent.parent);
	}

}
