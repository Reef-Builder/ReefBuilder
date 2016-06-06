using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * This script handles the main game objects, including the players currency and
 * the tracking of time.
 */
public class GameScript : MonoBehaviour {

	public static GameScript current = GameObject.FindObjectOfType<GameScript> ();

	// This is the text to update with the players currency amount.
	public Text polypText;
	public Text fossilText;
	// This is the text for the deletion toggle button.
	public Text deleteText; 

	// Keep this private so that only the GameScript can modify it.
	// This forces things to modify the polyps count through the provided
	// methods, which allows the text to be updated only when it needs to be.
	private int polyps = 1000;
	private int fossils = 5;
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

	private List<CoralScript> coral = new List<CoralScript> ();
	private List<FishScript> fish = new List<FishScript> ();
	private List<FishScript> eatingFish =new List<FishScript> ();

//	private HashSet<FishScript> eatingFish = new HashSet<FishScript> ();

	// Use this for initialization
	void Awake () {
		polypText.text = "" + polyps;
		fossilText.text = "" + fossils;

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
	
	
		if (coral.Count != 0 && gameCounter % 20 == 0 && fish.Count >0) {
			fishEat ();
		}

		if (gameCounter % 20 == 0 && eatingFish.Count > 0) {
			int i = UnityEngine.Random.Range(0,eatingFish.Count);
			FishScript f = eatingFish [i];
            if (f.isEating())
            {
                eatingFish.Remove(f);
                f.randFish();
            }
		    
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			addFossils (5);
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

	public int getPolyps() {
		return polyps;
	}

	public void addFossils(int count) {
		fossils = fossils + count;
		fossilText.text = "" + fossils;
	}

	public void removeFossils(int count) {
		fossils = fossils - count;
		fossilText.text = "" + fossils;
	}

	public int getFossils() {
		return fossils;
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

	public void addCoral(CoralScript coral) {
		this.coral.Add (coral);
		SoundManager.instance.AddRandomClip ();
	}

	public void addFish(FishScript fish) {
		this.fish.Add (fish);
	}

	public void fishEat(){
		int f =(int)  UnityEngine.Random.Range(0, fish.Count);
		FishScript fis = fish [f]; 
		int c =(int) UnityEngine.Random.Range(0, coral.Count);
		Transform target = coral [c].gameObject.transform;
		if (!eatingFish.Contains (fis) && fis != null) {
			eatingFish.Add (fis);
			fis.setTarget (target);
		}
	}

	public GameData Serialize() {
		GameData data = new GameData ();

		data.polyps = polyps;
		data.fossils = fossils;
		data.gameCounter = gameCounter;
		data.lastTime = lastTime;

		List<CoralData> coral = new List<CoralData> ();

		foreach(CoralScript c in this.coral) {
			coral.Add (c.Serialize());
		}

		data.coral = coral;

		return data;
	}

	public void Deserialize(GameData data) {
		polyps = data.polyps;
		polypText.text = "" + polyps;

		fossils = data.fossils;
		fossilText.text = "" + fossils;

		gameCounter = data.gameCounter;
		lastTime = data.lastTime;

		foreach (CoralData c in data.coral) {
			coral.Add (CoralScript.Deserialize(c));
		}
	}
}

/**
 * This represents a serialized version of the game, saving variables
 * to a separate and serializable-friendly class.
 */
[System.Serializable]
public class GameData {
	public int polyps;
	public int fossils;
	public int gameCounter;
	public DateTime lastTime;

	public List<FishData> fish;
	public List<CoralData> coral;
	public List<RockData> terrain;

	public override string ToString() {
		return "[GameData] polyps: " + polyps + ", fossils: " + fossils + ", gameCounter: "
		+ gameCounter + " + lastTime: " + lastTime;
	}
}