using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
* Spawns polyps around the player.  SpawnPolyps should be called
*/
public class SpawnPolyps : MonoBehaviour {

    public Transform polypPrefab;
    public float ejectForce = 5f;
    /** The amount of polyps to give the user per object they receive */
    public int polypsPerObject = 10;

    private int maxObjects = 5;
    private List<Transform> polyps;

	// Use this for initialization
	void Start () {
        polyps = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /** Spawns the given number of polyps around this object, for collection by the player. */
    public void createPolyps(int num){

        for(int i = 0; i < polyps.Count; i++)
        {
            if(polyps[i] == null)
            {
                polyps.Remove(polyps[i]);
                i--;
            }
        }

        for(int i = 0; i < num; i++) {

            float x = transform.rotation.eulerAngles.x - GetComponent<SnapToTerrain>().initialEulerRotation.x + Random.Range(-40, 40);
            float y = transform.rotation.eulerAngles.y - GetComponent<SnapToTerrain>().initialEulerRotation.y + Random.Range(-40, 40);
            float z = transform.rotation.eulerAngles.z - GetComponent<SnapToTerrain>().initialEulerRotation.z + Random.Range(-40, 40);

            if(polyps.Count >= maxObjects)
            {
                polyps[0].GetComponent<PolypScript>().SetPolypsReceived(polyps[0].GetComponent<PolypScript>().GetPolypsRecieved() + polyps[1].GetComponent<PolypScript>().GetPolypsRecieved());
                continue;
            }

            Transform polyp = (Transform)Instantiate(polypPrefab, transform.position, Quaternion.Euler(x, y, z));
            polyps.Add(polyp);
            //Now 'eject' the polyp
            polyp.transform.position += polyp.transform.forward * 0.1f;
            polyp.GetComponent<Rigidbody>().AddForce(polyp.transform.forward * ejectForce);
            polyp.GetComponent<PolypScript>().SetPolypsReceived(polypsPerObject);
        }
    }
}
