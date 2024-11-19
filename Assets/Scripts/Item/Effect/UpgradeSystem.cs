using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectActiveTime{
	private readonly EffectName name;
	private readonly List<float> activeTime = new List<float>();
	private readonly int levelMax;
	private int level = 0;

	public int LevelMax{
		get{ 
			return LevelMax;
		}
	}

	public EffectName Name{
		get{ 
			return name;
		}
	}

	public int Level{
		get{ 
			return level;
		}
	}

	public 	EffectActiveTime(EffectName name, int levelMax, float[] activeTime){
		this.name = name;
		this.levelMax = levelMax;
		if (activeTime.Length < levelMax) {
			Debug.LogError ("Active Time Not Sufficient for Max Level");
			return;
		}
		this.activeTime.AddRange (activeTime);
	}

	public virtual void LevelUp(){
		SetLevel (level + 1);
	}

	public virtual void SetLevel(int level){
		if (level < levelMax) {
			this.level = level;
		} else {
			Debug.LogWarning ("Level Exceeds Level Max");
		}
			
	}
//	public virtual bool isMax(){
//		return level == LevelMax;
//	}

	public virtual float GetActiveTime(){
		return activeTime [level];
	}

}

public class UpgradeSystem  {
	private List<EffectActiveTime> effects;

	public UpgradeSystem (){
		effects = new List<EffectActiveTime> ();
		CreateDataEffectActiveTime ();
	}

	public virtual void GetEffect(ref List<EffectName> names,ref List<int> levels){
		foreach (EffectActiveTime effect in effects) {
			names.Add (effect.Name);
			levels.Add (effect.Level);
		}
	}

	public virtual EffectName[] GetNamesEffect(){
		List<EffectName> names = new List<EffectName>();
		foreach (EffectActiveTime effect in effects) {
			names.Add (effect.Name);
		}
		return names.ToArray ();
	}

	public virtual int[] GetLevelsEffect(){
		List<int> levels = new List<int>();
		foreach (EffectActiveTime effect in effects) {
			levels.Add (effect.Level);
		}
		return levels.ToArray ();
	}

	public virtual int GetLevel(EffectName name){
		foreach (EffectActiveTime effect in effects) {
			if (effect.Name == name) {
				return effect.Level;
			}
		}
		Debug.LogWarning (name.ToString () + " is missing from List effect");
		return -1;
	}

	public virtual void LevelUp(EffectName name){
		foreach (EffectActiveTime effect in effects) {
			if (effect.Name == name) {
				effect.LevelUp ();
				return;
			}
		}
	}

	public virtual void SetLevel(EffectName name,int level){
		foreach (EffectActiveTime effect in effects) {
			if (effect.Name == name) {
				effect.SetLevel (level);
				return;
			}
		}
		Debug.LogWarning (name.ToString () + " is missing from List effect");
	}


	protected virtual void CreateDataEffectActiveTime(){

		effects.Add(new EffectActiveTime(EffectName.Magnet, 5, new float[]{5f,7f,10f,13f,17f}));
		effects.Add(new EffectActiveTime(EffectName.Shield, 5, new float[]{5f,7f,10f,13f,17f}));
	}



}
