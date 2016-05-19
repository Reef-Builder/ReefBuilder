using UnityEngine;
using System.Collections;

public class PolypScript : MonoBehaviour {

    public int polypsReceived = 1;
    private Vector3 rotation = new Vector3(90, 0, 0);
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(rotation);
       
	}

    public int GetPolypsRecieved() {
        return polypsReceived;
    }

    public void SetPolypsReceived(int received) {
        this.polypsReceived = received;
    }
}
