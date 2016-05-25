using UnityEngine;
using System.Collections;

[System.Serializable]
public class FishScript : MonoBehaviour {

    private float speed = 10;
    public float minSpeed = 10;
    public float maxSpeed = 10;

    private float xRot = -0.01f;
    public float yRot = 2f;
    private float zRot = 0f;

    private bool upSpeed = true;

    public bool positiveZ = false;
    public Vector3 initialMove = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
        transform.Translate(initialMove);
        speed = Random.Range(minSpeed, maxSpeed);

        if(Random.Range(0, 3) == 2)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            yRot = -yRot;
        }

        transform.Translate(0, Random.Range(-1, 15), 0);

	}
	
	// Update is called once per frame
	void Update () {

        float runtime = Time.time;
       
        float currYRot = (Mathf.Sin(runtime)+2f)*yRot;

       //upSpeed = (Random.Range(0, 10) > 7);

        if (upSpeed)
        {
            //speed += speed * 0.1f;
        } else
        {
           // speed -= speed * 0.2f;
        }

        if (positiveZ)
        {
            speed = -speed;
        }

        transform.Translate(0, 0, -speed * Time.deltaTime);
        transform.Rotate(new Vector3(xRot, currYRot, zRot) * Time.deltaTime);

        if (positiveZ)
        {
            speed = -speed;
        }

    }
}
