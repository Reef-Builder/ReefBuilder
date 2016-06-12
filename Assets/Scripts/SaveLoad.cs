using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad {

	public static GameData savedGame;

	public static void Save() {
		// Save the current game as serializable format
		SaveLoad.savedGame = GameScript.current.Serialize();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGame.gd");
		// Perform the serialization
		bf.Serialize(file, SaveLoad.savedGame);
		file.Close ();
	}

	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGame.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
			SaveLoad.savedGame = (GameData)bf.Deserialize(file);
			GameScript.current.Deserialize (SaveLoad.savedGame);
			file.Close();
		}
	}
}
