using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemsIcon", menuName = "ScriptableObjects/ItemsIcon")]
public class SOItemsIcon : ScriptableObject {
	[SerializeField] List<ItemIcon> items = new List<ItemIcon>();

	public virtual Sprite GetIcon(EffectName name){
		foreach (ItemIcon item in items) {
			if (item.name == name) {
				return item.icon;
			}
		}
		return null;
	}
}
[System.Serializable]
public class ItemIcon{
	public EffectName name;
	public Sprite icon;
}
