using UnityEngine;
using System.Collections;

public class FullControl : MonoBehaviour {

    private Vector3 forwardVec = new Vector3(1, 0, 0);
    private float speed = 2.0f;

    private float maxSideTilt = 45f;
    private float maxUpTilt = 30f;
    private float tiltSpeed = 20.0f;

    private float xRot;
    private float yRot;
    private float zRot;

    private float initialXRot;
    private float initialYRot;
    private float initialZRot;

    public bool active = false;

	// Use this for initialization
	void Start () {

        transform.Translate(new Vector3(0, 8, 0));

        xRot = transform.rotation.eulerAngles.x;
        yRot = transform.rotation.eulerAngles.y;
        zRot = transform.rotation.eulerAngles.z;

        initialXRot = xRot;
        initialYRot = yRot;
        initialZRot = zRot;
	}
	
	// Update is called once per frame
	void Update () {

        if (!active)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(xRot, yRot, zRot);

        transform.position += transform.right * speed * Time.deltaTime;

        float up = Input.GetAxis("Vertical");
        float left = Input.GetAxis("Horizontal");

        if(up > 0) {
            zRot -= Time.deltaTime * tiltSpeed;
        }

        if (up < 0) {
            zRot += Time.deltaTime * tiltSpeed;
        }

        if (left > 0){
            if (Mathf.Abs(xRot - initialXRot) < maxSideTilt)
            {
                xRot -= Time.deltaTime * tiltSpeed;
            }
            yRot += Time.deltaTime * tiltSpeed * 1.5f;
        }

        if (left < 0)
        {
            
            if (Mathf.Abs(xRot - initialXRot) < maxSideTilt) { 
                xRot += Time.deltaTime * tiltSpeed;
            }
            yRot -= Time.deltaTime * tiltSpeed * 1.5f;
        }

        if (left == 0)
        {
            xRot = Mathf.LerpAngle(xRot, initialXRot, Time.deltaTime * 0.8f);
        }
       // yRot = Mathf.LerpAngle(yRot, initialYRot, Time.deltaTime);
       // zRot = Mathf.LerpAngle(zRot, initialZRot, Time.deltaTime);

    }
}
