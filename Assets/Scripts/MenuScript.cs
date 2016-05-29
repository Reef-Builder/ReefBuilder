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

	private bool coralMenuVisible = true;

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
		SaveLoad.Load ();
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
