using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// The menu objects to toggle between
	public GameObject coralMenu;
	public GameObject miscMenu;

	// The toggle objects which switch between tabs
	public GameObject coralToggle;
	public GameObject miscToggle;

	public Color activeColor;
	public Color inactiveColor;

	private bool load = false;

	private bool coralMenuVisible = true;

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

	public void ReturnToMenu() {
		SceneManager.LoadScene ("Menu");
	}

	public void ExitGame() {
		Application.Quit();
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
