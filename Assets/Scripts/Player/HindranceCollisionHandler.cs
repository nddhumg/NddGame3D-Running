using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Player{
	public class HindranceCollisionHandler : NddBehaviour {
		[SerializeField] private Vector3 sizeBox;
		[SerializeField] private Vector3 sizeBoxCheck = new Vector3(1f, 1.8f, 0.2f); 
		[SerializeField] private Vector3 positionBoxCheck = new Vector3 (0f, 0f, 0.53f);

		[Header("Roll")]
		[SerializeField] private Vector3 sizeRollCheck =  new Vector3(1f, 1.3f, 0.2f); 
		[SerializeField] private Vector3 positionRollCheck = new Vector3(0f, -0.54f, 0.53f);

		[SerializeField] private LayerMask whatIsHindrance;
		public Action<RaycastHit> OnCollision;
		private RaycastHit hitCollision;
		[SerializeField] private float distanceRaycast = 0.2f;


		void Start(){
			sizeBox = sizeBoxCheck;
		}

		void FixedUpdate(){
			CheckHindranceCollision ();
		}

		void CheckHindranceCollision(){
			
			if (IsHindranceCollision() && GameManager.instance.IsPlay()) {
				OnCollision?.Invoke (hitCollision);
			}
		}

		public void SetSizeBox(bool isRoll){
			if (isRoll) {
				sizeBox = sizeRollCheck;
				transform.localPosition = positionRollCheck;
			} else {
				sizeBox = sizeBoxCheck;
				transform.localPosition = positionBoxCheck;
			}
		}

		bool IsHindranceCollision(){
			return Physics.BoxCast(transform.position, sizeBox*0.5f, transform.forward, out hitCollision, Quaternion.identity, distanceRaycast,whatIsHindrance);
		}                     

//		void OnDrawGizmos()
//		{
//			Gizmos.color = Color.red;
//			Gizmos.DrawRay(transform.position, transform.forward * distanceRaycast);
//			Gizmos.DrawWireCube(transform.position + transform.forward * distanceRaycast, sizeBox);
//		}

	}
}
