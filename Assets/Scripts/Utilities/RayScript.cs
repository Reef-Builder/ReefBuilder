using UnityEngine;
using System.Collections;

public class RayScript : MonoBehaviour {
	public float xRot = -0.01f;
	public float yRot = -.01f;
	public float zRot = 0f;
	public float speed =10;
	public float minSpeed =4;
	public float maxSpeed =5;
	public bool upSpeed = true;
	public bool positiveZ = true;
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


		if (UnityEngine.Random.Range (0f, 1) < .4) {
			transform.localScale = transform.localScale * UnityEngine.Random.Range (0.5f, 1.5f);
		} else {
			transform.localScale = transform.localScale * UnityEngine.Random.Range (0.8f, 1.2f);

		}


		transform.Translate(0, Random.Range(3, 15), 0);

		transform.localRotation.SetLookRotation (Vector3.right*90);

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

		transform.Translate(-speed * Time.deltaTime, 0,0 );
		transform.Rotate(new Vector3(xRot, currYRot, zRot) * Time.deltaTime);

		if (positiveZ)
		{
			speed = -speed;
		}
	}
}
