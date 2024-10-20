using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
	public class PlayerEffect : NddBehaviour {

		[Header("Equipment")]
		[SerializeField] protected GameObject equipmentShield;
		[SerializeField] protected GameObject equipmentMagnet;

		[SerializeField] protected GameObject magnetCollision;

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
			foreach (BaseEffect effect in effects) {
				if (effect == null)
					continue;
				effect.Update ();
			}
		}

		void FixedUpdate(){
			foreach (BaseEffect effect in effects) {
				if (effect == null)
					continue;
				effect.FixedUpdate ();
			}
		}

		void Start(){
			effects.Add (new ShieldEffect ());
			effects.Add (new MagnetEffect ());
	
		}

		public virtual void AddEffect(EffectName name){
			foreach (BaseEffect effect in effects) {
				if (effect.EffectName == name) {
					effect.Reset ();
				}
			}
		}




	}
}