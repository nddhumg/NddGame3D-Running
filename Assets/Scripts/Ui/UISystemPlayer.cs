using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystemPlayer : NddBehaviour {
	[SerializeField] protected GameObject ui;
	[SerializeField] protected List<UIUpgrade> upgrades;
	[SerializeField] protected SOItemsIcon itemsSO;

	[SerializeField] protected Button btnExit;

	void Start () {
		for (int i = 0; i < upgrades.Count; i++) {
			
			upgrades[i].Initialize ((EffectName)i, itemsSO.GetIcon((EffectName)i));
		}
		btnExit.onClick.AddListener (Exit);
		SetActive(false);
	}

	public virtual void SetActive(bool isActive){
		ui.SetActive (isActive);
	}

	protected virtual void Exit(){
		SetActive (false);
	}
}
