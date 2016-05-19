using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
* Spawns polyps around the player.  SpawnPolyps should be called
*/
public class SpawnPolyps : MonoBehaviour {
    /* The max amount of polyps allowed on the scene at any one time. */
    public static int maxPolypsOnScene = 10;
    public static int hardPolypLimit = 100;
    /* Chance that the max polyp limit will be lifted - this allows for random bonus polyp drops */
    public static float chanceOfLimitLift = 0.0001f;

    public Transform polypPrefab;
    /* The force at which the polyps will be ejected */
    public float ejectForce = 5f;
    /** The amount of polyps to give the user per object they receive */
    public int polypsPerObject = 10;
    public Vector3 normal = new Vector3(0, 0, 0);

    private int maxObjects = 5;
    private List<Transform> polyps;

    private long timeOfLastTouch = 0;
    private long bonusTickCount = 36000000000;
    private int bonusCount = 0;
    private int bonusMaxCount = 10;

	// Use this for initialization
	void Start () {
        polyps = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /** Spawns the given number of polyps around this object, for collection by the player.
    *  If the total number of polyp groups in the game exceed the max, none are created.  This
    * makes sure they don't pollute the scene and mean the player needs to collect the ones 
    * that are there. */
    public void createPolyps(int num){

       // if(Input.anyKey || Input.touchCount > 0)
       // {
       //     timeOfLastTouch = System.DateTime.UtcNow.Ticks;
       // }

        int totalPolyps = GameObject.FindObjectsOfType<PolypScript>().Length;

        float rand = Random.Range(0, 10000);
        bool liftLimit = false;
    
       // //if(System.DateTime.UtcNow.Ticks - timeOfLastTouch >= bonusTickCount)
        //{
         //   liftLimit = true;
         //   bonusCount++;

           // if(bonusCount > bonusMaxCount)
           // {
              //  bonusCount = 0;
                //liftLimit = false;
               // timeOfLastTouch = System.DateTime.UtcNow.Ticks;
           // }
       // }

        if(rand <= chanceOfLimitLift*10000f)
        {
            liftLimit = true;
        }

        if(!liftLimit && totalPolyps > maxPolypsOnScene)
        {
            return;
        }

        for(int i = 0; i < polyps.Count; i++)
        {
            if(polyps[i] == null)
            {
                polyps.Remove(polyps[i]);
                i--;
            }
        }

        for(int i = 0; i < num; i++) {

            float x = normal.x + Random.Range(-0.5f, 0.5f);
            float y = normal.y + Random.Range(-0.5f, 0.5f);
            float z = normal.z + Random.Range(-0.5f, 0.5f);

            Vector3 rot = new Vector3(x, y, z);

            if(polyps.Count >= maxObjects)
            {
                polyps[0].GetComponent<PolypScript>().SetPolypsReceived(polyps[0].GetComponent<PolypScript>().GetPolypsRecieved() + polyps[1].GetComponent<PolypScript>().GetPolypsRecieved());
                continue;
            }

            Transform polyp = (Transform)Instantiate(polypPrefab, transform.position, Quaternion.Euler(x, y, z));
            polyps.Add(polyp);
            //Now 'eject' the polyp
            polyp.transform.position += rot * 0.8f;
            polyp.GetComponent<Rigidbody>().AddForce(rot * ejectForce);
            polyp.GetComponent<PolypScript>().SetPolypsReceived(polypsPerObject);
        }
    }
}
