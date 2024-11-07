using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UpgradeData{
	public List<EffectName> names;
	public List<int> levels ;

	public UpgradeData(List<EffectName> names, List<int> levels ){
		this.names = names;
		this.levels = levels;
	}
}

namespace Player{
	public class PlayerEffect : NddBehaviour {

		[Header("Equipment")]
		[SerializeField] protected GameObject equipmentShield;

		[SerializeField] protected GameObject magnetCollision;

		[SerializeField] protected GameObject uiMagnet;
		[SerializeField] protected GameObject uiShield;

		protected List<BaseEffect> effects = new List<BaseEffect>();
		protected UpgradeSystem upgrade = new UpgradeSystem();

		public GameObject EquipmentShield{
			get{ 
				return equipmentShield;
			}
		}

		public GameObject MagnetCollision{
			get{ 
				return magnetCollision;
			}
		}

		void Update(){
			if (!GameManager.instance.IsPlay ())
				return;
			foreach (BaseEffect effect in effects) {
				if (effect == null)
					continue;
				effect.Update ();
			}
		}

		void FixedUpdate(){
			if (!GameManager.instance.IsPlay ())
				return;
			foreach (BaseEffect effect in effects) {
				if (effect == null)
					continue;
				effect.FixedUpdate ();
			}
		}

		void Start(){
			effects.Add (new ShieldEffect (uiShield));
			effects.Add (new MagnetEffect (uiMagnet));
	
		}

		public virtual void Save(ref UpgradeData data){
			data.names.Clear ();
			data.levels.Clear ();
			data.names.AddRange(upgrade.GetNamesEffect());
			data.levels.AddRange (upgrade.GetLevelsEffect());
		}

		public virtual void Load(UpgradeData data){
			int index = 0;
			foreach (EffectName name in data.names) {
				upgrade.SetLevel (name,data.levels[index]);
				index++;
			}
		}

		public int GetLevelEffect(EffectName name){
			return upgrade.GetLevel(name);
		}

		public virtual void LevelUp(EffectName name){
			upgrade.LevelUp (name);
		}

		public virtual void OffAllEffect(){
			foreach (BaseEffect effect in effects) {
				effect.End ();
			}
		}

		public virtual void AddEffect(EffectName name){
			foreach (BaseEffect effect in effects) {
				if (effect.EffectName == name) {
					effect.Reset ();
				}
			}
		}

		public virtual BaseEffect GetBaseEffectByName(EffectName name){
			foreach (BaseEffect effect in effects) {
				if (effect.EffectName == name) {
					return effect;
				}
			}
			Debug.LogWarning ("Dont base effect name: " + name);
			return null;
		}


	}
}