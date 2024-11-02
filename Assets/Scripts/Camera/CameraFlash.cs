using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : Singleton<CameraFlash> {
	[SerializeField] private Image imageFlash;
	[SerializeField] private Color colorFlash;

	void Start(){
		imageFlash.gameObject.SetActive (false);
	}
	public IEnumerator FlashCoroutine(float elapsedTime){
		imageFlash.gameObject.SetActive (true);
		float colerA = 1;
		colorFlash.a = colerA;
		imageFlash.color= colorFlash;
		float speed = 1 / elapsedTime;
		while(colerA > 0 ){
			colorFlash.a = colerA;
			imageFlash.color = colorFlash;
			colerA -= Time.deltaTime * speed;
			yield return null;
		}
		colorFlash.a = 0;
		imageFlash.color = colorFlash;
		imageFlash.gameObject.SetActive (false);
	}


	public void Flash(float elapsedTime){
		StartCoroutine (FlashCoroutine (elapsedTime));
	}

	public void Test(){
		
	}
}
