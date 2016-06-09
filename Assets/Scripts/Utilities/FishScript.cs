using UnityEngine;
using System.Collections;

[System.Serializable]
public class FishScript : MonoBehaviour {

    private float speed = 10;
    public float minSpeed = 10;
    public float maxSpeed = 10;

    private float xRot = -0.01f;
	//private float xRot = 0f;
	public float yRot = 2f;
    private float zRot = 0f;
	private float upYRot = -90f;
   

    public bool positiveZ = false;
    public Vector3 initialMove = new Vector3(0, 0, 0);
	public Transform spawnPoint;
	private Transform coral;
	private Vector3 target;

	const int RANDMODE = 1;
	const int MOVETOMODE = 2;
	const int EATMODE = 3;
	const int MOVEAWAYMODE = 5;
	const int MOVEUPMODE = 6;
	int mode = RANDMODE;
	float eatingDis = 2;
	const float disFormRock = 10;
	// Use this for initialization
	void Start () {

		//sqawns the fish behide the carmea 
		Transform carmera = GameObject.Find ("Main Camera").transform;
		Vector3 pos = carmera.transform.position-carmera.forward*5;
		transform.position = pos;
		transform.Translate(initialMove);
        speed = Random.Range(minSpeed, maxSpeed);






		if(Random.Range(0, 3) == 2)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            yRot = -yRot;
        }

        transform.Translate(0, Random.Range(5, 15), 0);

		if (UnityEngine.Random.Range (0f, 1) < .4) {
			transform.localScale = transform.localScale * UnityEngine.Random.Range (0.5f, 1.5f);
		} else {
			transform.localScale = transform.localScale * UnityEngine.Random.Range (0.8f, 1.2f);
		
		}
		upYRot = UnityEngine.Random.Range(-85,-95);
		
		
	}
	
	// Update is called once per frame
	void Update () {

	


		if (mode ==MOVETOMODE &&Vector3.Distance (transform.position, target)<=eatingDis) {
			mode = EATMODE;
		}
    	
		if(mode == MOVEAWAYMODE && Vector3.Distance (Vector3.zero, transform.position) > disFormRock) {
			mode = RANDMODE;
		}


		if (mode == RANDMODE) {
			randMove ();
			return;
		} 
		if (mode == MOVETOMODE) {
			moveTo ();
			return;
		}
		if (mode == MOVEAWAYMODE) {
			moveAway ();
			return;
		}

		//Debug.Log ("mode : " +mode);

	}


	public void randMove(){
		float runtime = Time.time;

		float currYRot = (Mathf.Sin(runtime)+2f)*yRot;

		//upSpeed = (Random.Range(0, 10) > 7);
		float cXRot = xRot;




		if (transform.position.y < 5) {
			cXRot = upYRot;
		}


		if (positiveZ){
			speed = -speed;
		}

		transform.Translate(0, 0, -speed * Time.deltaTime);
		transform.Rotate(new Vector3(cXRot, currYRot, zRot) * Time.deltaTime);

		if (positiveZ){
			speed = -speed;
		}


	}
	public void moveAway(){

		Vector3 angle = - (target-transform.position);
		Vector3 dir = new Vector3 (0,0,angle.z);
		float step = speed * Time.deltaTime;


		Vector3 rot = Vector3.RotateTowards (transform.forward, dir*10, step, 10f);

		//transform.position = Vector3.MoveTowards (transform.position, dir, step);
		transform.rotation = Quaternion.LookRotation (rot);


		transform.Translate(0, 0, speed * Time.deltaTime);

		if (transform.position.y == 10) {
			mode = RANDMODE;
		}

		//Debug.Log ("move away");
		//Debug.DrawRay (transform.position, dir, Color.green, 5, true);
	}



	public void moveTo(){

		RaycastHit hit;

	//	Vector3 dir = transform.TransformDirection (target)*100;

		Vector3 dir = (target-transform.position)*1000;
		Debug.DrawRay (transform.position,dir ,Color.red, 1, true);

		Physics.Raycast (transform.position, dir,out hit,1000);
			
		if (hit.collider == null) {
			randMove ();
			//Debug.Log ("returns null");
			return;
		}


	

		if (hit.collider.gameObject.transform.position==target) {
			//Debug.Log ("ray cast hit : "+hit.collider.gameObject);

			//upSpeed = (Random.Range(0, 10) > 7);


			Vector3 tarDir = target - transform.position;
			float step = speed * Time.deltaTime;


			Vector3 rot = Vector3.RotateTowards (transform.forward, tarDir, step, 0.0f);

			transform.rotation = Quaternion.LookRotation (rot);
			//	transform.Translate(transform.position*step );

			transform.position = Vector3.MoveTowards (transform.position, target, step);
	

			if (Vector3.Distance (transform.position, target) <= eatingDis) {
				mode = EATMODE;
				//Debug.Log ("Eat mode");
			}

		} else {
			randMove ();
			//Debug.Log (hit.collider.gameObject+" in way");
		
		}

	}

	public void setTarget(Transform v){
		target = v.position;
		coral = v;
		mode = MOVETOMODE;
		//Debug.Log ("target set: " + v);
	}

	public void randFish(){
		mode = MOVEAWAYMODE;
	
	}




}
