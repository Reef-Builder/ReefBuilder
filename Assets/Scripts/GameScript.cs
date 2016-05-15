using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/**
 * This script handles the main game objects, including the players currency and
 * the tracking of time.
 */
public class GameScript : MonoBehaviour {

	// This is the text to update with the players currency amount.
	public Text polypText;
	// This is the text for the deletion toggle button.
	public Text deleteText; 

	// Keep this private so that only the GameScript can modify it.
	// This forces things to modify the polyps count through the provided
	// methods, which allows the text to be updated only when it needs to be.
	private int polyps = 1000;
	private DateTime currentTime;
	private DateTime lastTime;

	// The tick variable causes the Games internal counter to increase by 1 
	// every 1 seconds. The value to tick every 1 second is 10000000. To change
	// the rate at which the game ticks over, this can be increased or decreased.
	private TimeSpan tick = new TimeSpan(1000000);

	// This is the internal gameCounter. This can be used to trigger certain things
	// when gameCounter reaches X. The gameCounter can also be tracked separately,
	// for example to trigger Y when gameCounter increases by X.
	private int gameCounter = 0;

	private bool deleteMode = false;

	// Use this for initialization
	void Start () {
		polypText.text = "" + polyps;

		// Use DateTime.Now to keep track of device time - easy!
		// Need to implement saving/loading so that when a game is loaded, time is included.
		// For now, will just keep track of current in-game time. 
		currentTime = DateTime.Now;
		lastTime = DateTime.Now;
	}

	// Update is called once per frame
	void Update () {
		currentTime = DateTime.Now;

		TimeSpan diffTime = currentTime.Subtract (lastTime);

		if (diffTime >= tick) {
			gameCounter++;
			lastTime = currentTime;
		}
	}

	public void addPolyps(int count) {
		polyps = polyps + count;
		polypText.text = "" + polyps;
	}

	public void removePolyps(int count) {
		polyps = polyps - count;
		polypText.text = "" + polyps;
	}

	public int getGameCounter() {
		return gameCounter;
	}

	public bool getDeleteMode() {
		return deleteMode;
	}

	public void toggleDeleteMode() {
		deleteMode = !deleteMode;

		if (deleteMode) {
			deleteText.text = "Place Coral";
		} else {
			deleteText.text = "Delete Coral";
		}
	}

}
