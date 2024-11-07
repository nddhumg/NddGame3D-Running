using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFront : MonoBehaviour {
	[SerializeField] protected float speed = 10f;

	void FixedUpdate () {
		if (!GameManager.instance.IsPlay ())
			return;
		Move ();
	}

	void Move(){
		transform.position +=  speed * Time.fixedDeltaTime * transform.forward;
	}
}
