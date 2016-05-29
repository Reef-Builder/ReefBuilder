using UnityEngine;
using System.Collections;
using System;

public class RockScript : MonoBehaviour, Placeable {

    public Sprite icon;
    public int cost;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getCost() {
        return cost;
    }

    public Sprite getIcon()
    {
        return icon;
    }

    public void setPlaced(bool place)
    {
        
    }
}
