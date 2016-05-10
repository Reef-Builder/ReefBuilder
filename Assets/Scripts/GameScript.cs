using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * This script handles the main game objects, including the players currency and
 * the tracking of time.
 */
public class GameScript : MonoBehaviour {

	// Keep this private so that only the GameScript can modify it.
	// This forces things to modify the polyps count through the provided
	// methods, which allows the text to be updated only when it needs to be.
	private int polyps = 1000;

	public Text polypText;

	// Use this for initialization
	void Start () {
		polypText.text = "" + polyps;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addPolyps(int count) {
		polyps = polyps + count;
		polypText.text = "" + polyps;
	}

	public void removePolyps(int count) {
		polyps = polyps - count;
		polypText.text = "" + polyps;
	}

}
