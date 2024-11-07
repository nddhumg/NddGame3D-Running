using UnityEngine;

public class Coin : MonoBehaviour ,IPickUpAble, IMagnetAble {
	[SerializeField] private uint value = 1;

	[Header("Magnet")]
	[SerializeField] private float speedMagnetAble = 0.1f;
	[SerializeField] private bool isMagnet = false;
	protected Vector3 positionToMagnet;
	void FixedUpdate(){
		if (isMagnet) {
			positionToMagnet = Player.PlayerManager.instance.transform.position;
			transform.position = Vector3.Lerp (transform.position, positionToMagnet, speedMagnetAble * Time.fixedDeltaTime);
		}
	}

	void OnDisable(){
		isMagnet = false;
	}

	public virtual void PickUpAble(GameObject obj){
		GameController.instance.Coin += value;
	}

	public virtual void MagnetAble(){
		isMagnet = true;
	}

	public virtual void DestroyOnPickUp(){
		gameObject.SetActive (false);
	}
}
