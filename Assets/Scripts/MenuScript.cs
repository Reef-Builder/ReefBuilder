using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// The menu objects to toggle between
	public GameObject coralMenu;
	public GameObject miscMenu;
	public GameObject gameMenu;
	public GameObject gameMenuExpanded;
	public GameObject resourceMenu;

	// The toggle objects which switch between tabs
	public GameObject coralToggle;
	public GameObject miscToggle;

	public Color activeColor;
	public Color inactiveColor;

	private bool load = false;
	private bool coralMenuVisible = true;
	private bool menuOpen = false;

	// Use this for initialization
	void Awake () {
		GameScript.current = GameObject.FindObjectOfType<GameScript> ();
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame() {
		SceneManager.LoadScene ("MainGame");
	}

	public void LoadGame() {
		SceneManager.LoadScene ("MainGame");
		load = true;
	}

	public void OnLevelWasLoaded(int level) {
		if (load) {
			SaveLoad.Load ();
			load = false;
		}
	}

	public void SaveGame() {
		SaveLoad.Save ();
	}

	public void SaveAndExit() {
		SaveLoad.Save ();
		SceneManager.LoadScene ("Menu");
	}

	public void ReturnToMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void DisplayGame() {
		gameMenuExpanded.SetActive (false);
		gameMenu.SetActive (true);
		resourceMenu.SetActive (true);
		menuOpen = false;
	}

	public void DisplayMenu() {
		gameMenuExpanded.SetActive (true);
		gameMenu.SetActive (false);
		resourceMenu.SetActive (false);
		menuOpen = true;
	}

	public void ToggleGameMenu() {
		// The menu is currently open, so should be closed!
		if (menuOpen) {
			DisplayGame ();
		} else {
			DisplayMenu ();
		}

	}

	public void SwitchMenuMode(bool mode) {
		coralMenuVisible = mode;

		if (coralMenuVisible) {
			coralMenu.SetActive (true);
			miscMenu.SetActive (false);

			coralToggle.GetComponent<Image> ().color = activeColor;
			miscToggle.GetComponent<Image> ().color = inactiveColor;
		} else {
			coralMenu.SetActive (false);
			miscMenu.SetActive (true);

			coralToggle.GetComponent<Image> ().color = inactiveColor;
			miscToggle.GetComponent<Image> ().color = activeColor;
		}
	}
}
