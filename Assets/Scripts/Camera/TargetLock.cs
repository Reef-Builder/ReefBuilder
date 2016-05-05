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

        if (init && !Input.GetMouseButtonDown(0) && (Input.mousePosition - touchPos).magnitude < 5)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //If it's the right kind of object..
                if(hit.transform.GetComponent<CameraCanSnapTo>() != null)
                {
                    GetComponent<MouseOrbit>().target = hit.transform.gameObject.transform;
                    Debug.Log(hit.transform.gameObject.name);
                }
            }
        }

     

    }
}
