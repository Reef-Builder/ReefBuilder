using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoralScript : MonoBehaviour {

	// The UI icon to display for this coral.
	public Sprite icon;
	// The cost of this coral
	public int cost;
	// The number of game ticks to wait before spawning currency.
	public int spawnRate;
	// The number of currency to spawn every tick.
	public int spawnAmount;
	// The number of game ticks to wait before spawning a fish.
	public int fishRate;
	// The fish prefab which this coral should spawn.
	public Transform fishPrefab;

	private GameScript gameScript;

	// The instantiated fish itself.
	private Transform fish;

	// These keep track of the internal game counter. When the difference between
	// the gameCounter and the script specific counter reaches a certain rate, something
	// will happen and the counter is reset.
	private int spawnCounter;
	private int fishCounter;

	// For performance reasons, only one fish will be spawned per coral. A fish is still 
	// waiting to be spawned if this is false.
	private bool fishSpawned = false;

	// Use this for initialization
	void Start () {
		gameScript = GameObject.Find ("GameController").GetComponent<GameScript> ();
		spawnCounter = gameScript.getGameCounter ();
		fishCounter = spawnCounter;
	}
	
	// Update is called once per frame
	void Update () {
		int gameCounter = gameScript.getGameCounter ();


		if(!fishSpawned && ((gameCounter - fishCounter) >= fishRate)) {
			fishCounter = gameCounter;
			spawnFish ();
		}

		if ((gameCounter - spawnCounter) >= spawnRate) {
			spawnCounter = gameCounter;
			gameScript.addPolyps (spawnAmount);
		}
	}

	public int getCost() {
		return cost;
	}

	private void spawnFish() {
		fish = (Transform)Instantiate (fishPrefab, Vector3.zero, Quaternion.identity);

		print (fish);

		fishSpawned = true;
	}
}
