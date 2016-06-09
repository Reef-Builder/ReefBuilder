using UnityEngine;
using System.Collections;

public class BackGroundFishScript : MonoBehaviour {
	private float speed = 10;
	public float minSpeed = 10;
	public float maxSpeed = 10;
	private float xRot = -0.01f;
	public bool positiveZ = false;
	public float yRot = 4f;

	private float zRot = 0f;
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

		transform.Translate(0, Random.Range(5, 15), 0);

	}
	
	// Update is called once per frame
	void Update () {
		randMove ();
	}


	public void randMove(){
		float runtime = Time.time;

		float currYRot = (Mathf.Sin(runtime)+2f)*yRot;

		//upSpeed = (Random.Range(0, 10) > 7);
		float cXRot = xRot;

		if (transform.position.y < 10) {
			cXRot = -.90f;
			Debug.Log ("fish too low");
		}



		

		if (positiveZ){
			speed = -speed;
		}

		transform.Translate(0, 0, -speed * Time.deltaTime);
		transform.Rotate(new Vector3(cXRot, currYRot, zRot) * Time.deltaTime, Space.World);

		if (positiveZ){
			speed = -speed;
		}


	}


}
