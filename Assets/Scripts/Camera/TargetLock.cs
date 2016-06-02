using UnityEngine;
using System.Collections;

public class TargetLock : MonoBehaviour {

    private Vector3 touchPos = Vector2.zero;
    private bool init = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
            init = true;
        }

        if (init && Input.GetMouseButtonDown(0) && (Input.mousePosition - touchPos).magnitude < 10)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
            RaycastHit[] rays = Physics.RaycastAll(ray, 1000);
            float minDist = float.MaxValue;
            Transform closest = null;

            foreach(RaycastHit hit in rays)
            {

                if (hit.transform.gameObject.GetComponent<CoralScript>() == null && hit.distance < minDist)
                {
                    minDist = hit.distance;
                    closest = hit.transform;
                }

        
            }
            if (closest != null && closest != Camera.main.GetComponent<MouseOrbit>().target)
            {

                if(closest.GetComponent<CameraCanSnapTo>() == null)
                {
                    return;
                }

                MouseOrbit orbit = Camera.main.GetComponent<MouseOrbit>();
                orbit.FlyTo(closest);

               // Camera.main.GetComponent<MouseOrbit>().distance = 15;
            }

        }

     

    }
}
