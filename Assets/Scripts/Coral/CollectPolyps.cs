using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectPolyps : MonoBehaviour {

    private GameScript gameScript;
    private bool clicked;
    private Vector3 touchPos;

    public Transform polypCountElement;

    public List<Transform> collectingPolyps;
    private float collectSpeed = 10;

    // Use this for initialization
    void Start () {
        gameScript = GameObject.Find("GameController").GetComponent<GameScript>();
        collectingPolyps = new List<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
            clicked = true;
        }

        if (clicked && !Input.GetMouseButton(0) && (Input.mousePosition - touchPos).magnitude < 10)
        {
            clicked = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] rays = Physics.RaycastAll(ray, 1000);

            foreach (RaycastHit hit in rays)
            {
                //If it's the right kind of object..
                if (hit.transform.GetComponent<PolypScript>() != null)
                {
                    collectingPolyps.Add(hit.transform);
                }
            }
        }

        for(int i = 0; i < collectingPolyps.Count; i++) {
            //Find vector of this polyp to the ui element
            if(collectingPolyps[i] == null)
            {
                collectingPolyps.RemoveAt(i);
                continue;
            }
            Vector3 uiPosition = polypCountElement.position;

            float distance = (collectingPolyps[i].position - uiPosition).magnitude;

            collectingPolyps[i].transform.position = Vector3.MoveTowards(collectingPolyps[i].transform.position, uiPosition, collectSpeed*Time.deltaTime);

            if(distance < 1)
            { 
                gameScript.addPolyps(collectingPolyps[i].GetComponent<PolypScript>().GetPolypsRecieved());
                Transform t = collectingPolyps[i];
                collectingPolyps.Remove(t);
                Destroy(t.gameObject, 0);
            }
        }

       

    }
}
