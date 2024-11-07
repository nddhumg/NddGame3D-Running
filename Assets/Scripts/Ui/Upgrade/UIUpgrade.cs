using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : NddBehaviour {
	[SerializeField] protected Image[] imageLevel;
	[SerializeField] protected Image icon;
	[SerializeField] protected Button btnLevelUp;
	[SerializeField] protected EffectName effectName;

	[SerializeField] protected Color colorLevel;
	[SerializeField] protected Color colorLevelBase;

	void Start(){
		foreach (Image img in imageLevel) {
			img.color = colorLevelBase;
		}
		ChangeLevelUp ();
		btnLevelUp.onClick.AddListener (OnBtnLevelUp );
	}

	public void Initialize(EffectName name, Sprite icon){
		effectName = name;
		this.icon.sprite = icon;
	}

	void OnBtnLevelUp(){
		Player.PlayerManager.instance.PlayerEffect.LevelUp (effectName);
		ChangeLevelUp ();
	}

	void ChangeLevelUp(){
		int level = Player.PlayerManager.instance.PlayerEffect.GetLevelEffect (effectName);
		for (int indexImage = 0; indexImage <= level; indexImage++) {
			imageLevel [indexImage].color = colorLevel;
		}
	}

}
