using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoralScript : MonoBehaviour {

	// The UI icon to display for this coral.
	public Sprite icon;
	// The cost of this coral
	public int cost;
	// The number of game ticks to wait before spawning currency
	public int spawnRate;
	// The number of currency to spawn every tick.
	public int spawnAmount;

	public GameObject fish;

	private GameScript gameScript;
	private int internalCounter = 0;

	private int growRate = 100;
	private int growCounter;
	private float growIncrement = 0.1f;
	private float maxScale = 1.0f;

	// Use this for initialization
	void Start () {
		gameScript = GameObject.Find ("GameController").GetComponent<GameScript> ();
		growCounter = gameScript.getGameCounter ();
	}
	
	// Update is called once per frame
	void Update () {
		int gameCounter = gameScript.getGameCounter ();
		if ((gameCounter - internalCounter) > spawnRate) {
			internalCounter = gameCounter;

			gameScript.addPolyps (spawnAmount);
		}

		if ((transform.localScale.x <= maxScale) && (gameCounter - growCounter) > growRate) {
			growCounter = gameCounter;

			Vector3 scale = transform.localScale;

			scale = new Vector3 (scale.x + growIncrement, scale.y + growIncrement, scale.z + growIncrement);
			transform.localScale = scale;
		}
	}

	public int getCost() {
		return cost;
	}

	public void OnMouseOver() {
		if(Input.GetMouseButtonDown(0) && gameScript.getDeleteMode()) {
			gameScript.addPolyps ((cost / 2));
			Destroy (gameObject);
		}
	}
}
