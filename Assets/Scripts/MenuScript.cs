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

	private int game = 0;
	private bool coralMenuVisible = true;
	private bool menuOpen = false;

    private float originalAnchorMinY;
    public float anchorMinYToRemoveWhenDisabled = 0.005f;

    // Use this for initialization
    void Start () {
		DontDestroyOnLoad(transform.gameObject);

        originalAnchorMinY = coralToggle.GetComponent<RectTransform>().anchorMin.y;

        coralMenuVisible = !coralMenuVisible;
        SwitchMenuMode(!coralMenuVisible);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame(int game) {
		SceneManager.LoadScene ("MainGame");
        this.game = game;
	}

	public void LoadGame(int game) {
        this.game = game;

        PersistentSettings.Instance.SaveGameId = this.game;

		SceneManager.LoadScene ("MainGame");    
    }

	public void SaveCurrentGame() {
		SaveLoad.Save (game);
	}

	public void SaveAndExit() {
		SaveLoad.Save (game);
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

	public void SwitchMenuMode(bool coralMenuVisible) {

        if(coralMenuVisible == this.coralMenuVisible)
        {
            return;
        }

		this.coralMenuVisible = coralMenuVisible;

        RectTransform enabledPanel = null;
        RectTransform disabledPanel = null;

        if (coralMenuVisible) {
			coralMenu.SetActive (true);
			miscMenu.SetActive (false);

			coralToggle.GetComponent<Image>().color = activeColor;
			miscToggle.GetComponent<Image>().color = inactiveColor;

            enabledPanel = coralToggle.GetComponent<RectTransform>();
            disabledPanel = miscToggle.GetComponent<RectTransform>();
        } else {
			coralMenu.SetActive (false);
			miscMenu.SetActive (true);

			coralToggle.GetComponent<Image> ().color = inactiveColor;
			miscToggle.GetComponent<Image> ().color = activeColor;

            enabledPanel = miscToggle.GetComponent<RectTransform>();
            disabledPanel = coralToggle.GetComponent<RectTransform>();
        }

        enabledPanel.anchorMin = new Vector2(enabledPanel.anchorMin.x, originalAnchorMinY);
        disabledPanel.anchorMin = new Vector2(disabledPanel.anchorMin.x, originalAnchorMinY + anchorMinYToRemoveWhenDisabled);
    }
}
