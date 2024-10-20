using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField] private Vector3 offset;
	[SerializeField] private Transform target;
	[SerializeField] private float speed = 10f;

	void Start(){
		offset = transform.position - target.position;
	}

	void FixedUpdate(){
		Vector3 newPosition = transform.position;
		newPosition.x = offset.x + target.position.x;
		newPosition.z = offset.z + target.position.z;
		transform.position = Vector3.Lerp (transform.position, newPosition, speed * Time.fixedDeltaTime);
	}

}
