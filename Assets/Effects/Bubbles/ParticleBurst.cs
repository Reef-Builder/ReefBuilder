using UnityEngine;
using System.Collections;

public class ParticleBurst : MonoBehaviour {

	public Transform particleSystemPrefab;
	private Transform instance;
	private ParticleSystem system;

	private SnapToTerrain snapScript = null;
	private bool placed = false;

	// Use this for initialization
	void Start () {
		instance = Instantiate (particleSystemPrefab);
		system = instance.GetComponent<ParticleSystem> ();

		snapScript = GetComponent<SnapToTerrain> ();

		instance.position = transform.position - snapScript.positionOffset;
		instance.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!placed && snapScript.isLocked ()) {
			instance.parent = null;

			system.Play();
			placed = true;
			//system.emission.enabled = true;
		}
	}
}
