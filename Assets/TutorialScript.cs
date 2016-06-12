using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    private GameScript gameScript;
    public bool playTutorial = true;

    private int index = 1;
    private int newIndex = 1;

    public float waitTime = 2;
    private float achievedTime = 0;

    private Transform target;

	// Use this for initialization
	void Start () {

        target = GameObject.FindObjectOfType<MouseOrbit>().target;
        gameScript = GameObject.FindObjectOfType<GameScript>();

        //Start off by disabling all children.
        DisableAllPrompts();

        if (playTutorial) {
            EnablePrompt("Tutorial1");
        }

	}
	
	// Update is called once per frame
	void Update () {

        CoralScript[] corals = GameObject.FindObjectsOfType<CoralScript>();
        if (newIndex == 1 && corals.Length != 0) {
            achievedTime = Time.time;
            newIndex++;
        } else if(index == 2 && newIndex == 2){
            newIndex++;
            achievedTime = Time.time+4;
        } else if(index == 3 && newIndex == 3) {
            GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
            if(rocks.Length > 1) {
                achievedTime = Time.time;
                newIndex++;
            }
        } else if(index == 4 && newIndex == 4) {
            MouseOrbit orbit = GameObject.FindObjectOfType<MouseOrbit>();
            if(orbit.target != target){
                achievedTime = Time.time-1.5f;
                newIndex++;
                gameScript.addFossils(5);
            }
        } else if(index == 5 && newIndex == 5) {
            achievedTime = Time.time + 2f;
            newIndex++;
        }

        if(newIndex != index && Time.time - achievedTime > waitTime) {
            index = newIndex;
            DisableAllPrompts();
            EnablePrompt("Tutorial" + index);
        }

	}

    private void EnablePrompt(string name) {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Equals(name)) {
                child.gameObject.SetActive(true);
                return;
            }
        }
    }

    private void DisableAllPrompts()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
