using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System{
	public class SaveLoadSystem {
		private static SaveData saveData = new SaveData();
		private static IDataService dataService = new FileDataService (new JsonSerializer (), "json");

		[System.Serializable]
		public class SaveData  {
			public GameData gameData;
			public UpgradeData upgradeData;

			public SaveData(){
				gameData = new GameData();
				upgradeData  = new UpgradeData(new List<EffectName>(),new List<int>());
			}
		}

		public static void SaveGame(){
			HandleSaveData ();
			dataService.Save <SaveData>(ref saveData);

		}

		private static void HandleSaveData(){
			GameManager.instance.Save (ref saveData.gameData);
			Player.PlayerManager.instance.PlayerEffect.Save (ref saveData.upgradeData);
		}

		public static void LoadGame(){
			saveData = dataService.Load<SaveData> (typeof(SaveData).Name);
			HandleLoadData ();
		}	

		private static void HandleLoadData(){
			GameManager.instance.Load (saveData.gameData);
			Player.PlayerManager.instance.PlayerEffect.Load (saveData.upgradeData);

		}
	}
}
