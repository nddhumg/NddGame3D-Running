using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHindrance : Hindrance {
	[SerializeField] protected Vector3 positionAnchor;
	void Start(){
		positionAnchor = transform.localPosition;
	}

	protected override void OnDisable(){
		base.OnDisable ();
		ResetPosition ();
	}

	protected virtual void ResetPosition(){
		transform.localPosition = positionAnchor;
	}
}
