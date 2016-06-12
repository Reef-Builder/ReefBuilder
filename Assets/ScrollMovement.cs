using UnityEngine;
using System.Collections;

public class ScrollMovement : MonoBehaviour {

    public Vector3 moveVec;
    private Vector3 startPosition;
    private float mag;

    public float moveSpeed = 1;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        mag = (moveVec - startPosition).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += moveVec * moveSpeed * Time.deltaTime;
	    if((transform.position - startPosition).magnitude >= mag) {
            transform.position = startPosition;
        }
	}
}
