using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
	public class PlayerEffect : NddBehaviour {

		[Header("Equipment")]
		[SerializeField] protected GameObject equipmentShield;

		[SerializeField] protected GameObject magnetCollision;

		[SerializeField] protected GameObject uiMagnet;
		[SerializeField] protected GameObject uiShield;

		private List<BaseEffect> effects = new List<BaseEffect>();

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