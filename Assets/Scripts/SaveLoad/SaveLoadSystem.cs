using System.IO;
using System;
using UnityEngine;

namespace System{
	public class SaveLoadSystem {
		private static SaveData saveData= new SaveData();
		static IDataService dataService = new FileDataService (new JsonSerializer (), "json");

		[System.Serializable]
		public struct SaveData  {
			public GameData gameData;
		}

		public static void SaveGame(){
			HandleSaveData ();
			dataService.Save <SaveData>(ref saveData);

		}

		private static void HandleSaveData(){
			GameManager.instance.Save (ref saveData.gameData);
		}

		public static void LoadGame(){
			saveData =  dataService.Load<SaveData> (typeof(SaveData).Name);
			HandleLoadData ();
		}	

		private static void HandleLoadData(){
			GameManager.instance.Load (saveData.gameData);
		}
	}
}
