using UnityEngine;
using System.Collections;

public class PathFollow : MonoBehaviour {

	// Use this for initialization
	public string path;
	public float speed=1;

	void Start () {
		iTween.PutOnPath (gameObject, iTweenPath.GetPath (path), 0f);
		iTween.MoveTo(gameObject,iTween.Hash("path", iTweenPath.GetPath(path),"speed",speed, "easeType",iTween.EaseType.linear, "Orienttopath", true, "looptype",iTween.LoopType.loop));
	}

	void FixedUpdate(){
		


	
	}



	// Update is called once per frame
	void Update () {
	
	}
}
