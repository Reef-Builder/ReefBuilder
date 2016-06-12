using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[System.Serializable]
public class CoralScript : MonoBehaviour, Placeable {

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
    // Add polyps manually, or auto
    public bool manualCoralCollection;
	// Can this be placed on sand?
	public bool sandPlacement = false;

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
	//private bool placed = false;

	private int growRate = 20;
	private int growCounter;
	private Vector3 originalScale;
	private float growIncrement = 0.1f;
	private float relativeSize = 1.0f;
	private float maxScale = 2.0f;

	// Use this for initialization
	void Start () {
		gameScript = GameObject.Find ("GameController").GetComponent<GameScript> ();
		spawnCounter = gameScript.getGameCounter ();
		fishCounter = spawnCounter;
		growCounter = gameScript.getGameCounter ();
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		/*print (placed);
		if (!placed) {
			return;
		}*/

		//print ("test");

		int gameCounter = gameScript.getGameCounter ();

		if(!fishSpawned && ((gameCounter - fishCounter) >= fishRate)) {
			fishCounter = gameCounter;
			spawnFish ();
		}

		if ((gameCounter - spawnCounter) >= spawnRate) {
			spawnCounter = gameCounter;
            if (manualCoralCollection) {
                GetComponent<SpawnPolyps>().createPolyps(spawnAmount);
            } else {
                gameScript.addPolyps(spawnAmount * GetComponent<SpawnPolyps>().polypsPerObject);
            }	
		}

		if ((relativeSize < maxScale) && (gameCounter - growCounter) >= growRate) {
			growCounter = gameCounter;

			transform.localScale = originalScale * relativeSize;
			relativeSize = relativeSize + growIncrement;
		}
	}

	public int getCost() {
		return cost;
	}

	private void spawnFish() {
		fish = (Transform)Instantiate (fishPrefab, Vector3.zero, Quaternion.identity);
		fishSpawned = true;

		gameScript.addFish (fish.GetComponent<FishScript> ());
	}

	public void setPlaced(bool place) {
		//print (place);
		//placed = place;
	}

	public void OnMouseOver() {
		if(Input.GetMouseButtonDown(0) && gameScript.getDeleteMode()) {
			gameScript.addPolyps ((cost / 2));
			Destroy (gameObject);
		}
	}

    public Sprite getIcon() {
        return icon;
    }

	public bool canPlaceOnSand() {
		return sandPlacement;
	}

	public CoralData Serialize() {
		CoralData data = new CoralData ();

		data.prefab = transform.name.Replace ("(Clone)", "");

		data.relativeSize = relativeSize;

		data.localPositionX = transform.localPosition.x;
		data.localPositionY = transform.localPosition.y;
		data.localPositionZ = transform.localPosition.z;

		data.localScaleX = transform.localScale.x;
		data.localScaleY = transform.localScale.y;
		data.localScaleZ = transform.localScale.z;

		data.localEulerAnglesX = transform.localEulerAngles.x;
		data.localEulerAnglesY = transform.localEulerAngles.y;
		data.localEulerAnglesZ = transform.localEulerAngles.z;

		return data;
	}

	public static CoralScript Deserialize(CoralData data) {
		GameObject coral = (GameObject)Instantiate (Resources.Load("Prefabs/GameObjects/" + data.prefab));

		SnapToTerrain snapScript = coral.GetComponent<SnapToTerrain> ();
		snapScript.HardLock (true);

		coral.transform.localPosition = new Vector3(data.localPositionX, data.localPositionY, data.localPositionZ);
		coral.transform.localScale = new Vector3(data.localScaleX, data.localScaleY, data.localScaleZ);
		coral.transform.localEulerAngles = new Vector3(data.localEulerAnglesX, data.localEulerAnglesY, data.localEulerAnglesZ);

		CoralScript coralScript = coral.GetComponent<CoralScript> ();

		coralScript.relativeSize = data.relativeSize;

		return coralScript;
	}
}


[System.Serializable]
public class CoralData {
	public String prefab;

	public float relativeSize;

	public float localPositionX;
	public float localPositionY;
	public float localPositionZ;

	public float localScaleX;
	public float localScaleY;
	public float localScaleZ;

	public float localEulerAnglesX;
	public float localEulerAnglesY;
	public float localEulerAnglesZ;
}