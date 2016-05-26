using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame() {
		SceneManager.LoadScene ("MainGame");
	}

	public void LoadGame() {
		SaveLoad.Save ();
		print (SaveLoad.savedGames);
		SaveLoad.Load ();
		print (SaveLoad.savedGames);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
