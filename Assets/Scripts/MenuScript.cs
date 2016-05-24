using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame() {
		print ("start new game");
	}

	public void LoadGame() {
		print ("you what");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
