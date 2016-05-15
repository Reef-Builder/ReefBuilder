using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour {
    private static int NORAML=0;
    private static int EAT = 1;
    private static int FLEA = 2;
    public int state = NORAML;
    public LayerMask foodChain = 0;
    public Collider vision;

    public float currentSpeed;
    public float max_Speed;
    public float nor_Speed;
    public float fleaTime = 50;
    private float timer = 0;
    private bool inDanger = false;
    private float radius = 0.05f;
    private float zone = 5;
    public Vector3 goal;

    // Use this for initialization
    void Start () {
	//	iTween.RotateBy(gameObject, iTween.Hash("x", .25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));

		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("path00"),"speed",nor_Speed, "easetype",iTween.EaseType.linear, "orienttopath", true, "lookahead\t",.01f,"isLocal",true));    
        
    }

    void FixedUpdate()
	{
		
	}

    

   	// Update is called once per frame
	void Update () {
        
         
	}

    void OnTriggerEnter(Collider c) {
    

    }

    void OnTriggerExit() {
       

    }


    

}
