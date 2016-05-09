using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoralScript : MonoBehaviour {

	public Sprite icon;
	public int cost;

	public GameObject fish;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int getCost() {
		return cost;
	}
}
