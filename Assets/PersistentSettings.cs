using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSettings : Singleton<PersistentSettings> {

    public int SaveGameId = -1;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
