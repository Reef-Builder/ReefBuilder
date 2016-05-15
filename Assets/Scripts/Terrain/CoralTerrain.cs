using UnityEngine;
using System.Collections;

/** 

    This script should be placed on objects you wish to use as 
    terrain for coral placement.

    Check whether objects have this script by checking that (object.GetComponent<CoralTerrain>() != null)

    This script also allows the user to move the piece coral terrain lcoation around the reef that they are on, in
    a maximum of two dimensions at the current time (up, down, left, right)

*/
public class CoralTerrain : MonoBehaviour {

    private Vector3 moveVec = new Vector3(0, 0.1f, 0);

    public float maxHeight = 20;
    public float minHeight = 0;

	void Start() {
		
	}

	void FixedUpdate() {
        print(Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0 && transform.position.y < maxHeight)
        {
            transform.Translate(moveVec);
        } else if (Input.GetAxis("Vertical") < 0 && transform.position.y > minHeight)
        {
            transform.Translate(-moveVec);
        }
	}
}
