using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCamera : MonoBehaviour {

    private float lastShot;
    private float shotsPerSecond = 0.1f;
    private List<Texture2D> lastTenSeconds = new List<Texture2D>();

    private int resWidth = 200;
    private int resHeight = 200;

    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastShot >= 1/shotsPerSecond)
        {
            lastShot = Time.time;

            var rt = new RenderTexture(resWidth, resHeight, 24);

            cam.targetTexture = rt;

            var screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

            cam.Render();

            RenderTexture.active = rt;

            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

            cam.targetTexture = null;
            
            RenderTexture.active = null; // JC: added to avoid errors

            Destroy(rt);

            SaveLoad.SaveImage(screenShot, 0);
        }
	}
}
