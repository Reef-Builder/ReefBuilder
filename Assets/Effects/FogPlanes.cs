using UnityEngine;
using System.Collections;

/** 
    Creates and handles fog planes.

    This script emulates fog by creating a number
    of colored planes, whose alpha value is changed based 
    on the cameras distance to the camera.

    A single plane is also placed directly in front of the camera, whose alpha value 
    goes from high -> low as you zoom in.
    Notes:

    Should always be attached to a Camera.

    SetTarget(Transform) needs to be called on this script
    to set the target the camera is currently rotating around.
    If the target is not set, fog will not appear.


*/
public class FogPlanes : MonoBehaviour {

    private Transform target;

    /* The number of fog planes to use */
    public int numFogPlanes = 15;

    public float minDist = 20f;

    public float gapBetweenPlanes = 1f;

    /* The higher the lower the transparency of each plane */
    public float maxDistance = 500f;

    /* The color of the fog */
    public Color fogColor;
    public Material material;
    private GameObject[] planes;

	// Create the fog planes with a default alpha value of 0
    // Plane locations, rotations and scales are set in Update.
	void Start () {
        planes = new GameObject[numFogPlanes];

        for(int i = 0; i < numFogPlanes; i++){
            planes[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            planes[i].GetComponent<Renderer>().material = new Material(material);

            planes[i].GetComponent<Renderer>().material.color = fogColor;
            planes[i].GetComponent<Collider>().enabled = false;
        }

	}

    void applyAlpha(GameObject obj, float alpha) {

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = new Color(1, 1, 1, alpha);
        }
        mesh.colors = colors;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (target == null) {
            return;
        }

        for(int i = 0; i < numFogPlanes; i++) {  

            //First, translate the planes so that they are at even intervals
            float dist = gapBetweenPlanes * i;

            //For our last plane, place it right in front of the camera.
            if (i == numFogPlanes - 1)
            {
              //  dist = (transform.position - planes[i].transform.position).magnitude-1;
            }

            planes[i].transform.position = target.transform.position + (transform.forward * -dist) + (transform.forward * minDist);

            //If the target is behind the camera, disable it's renderer for this frame.
            if (!planes[i].GetComponent<Renderer>().isVisible)
            {
                continue;
               // planes[i].GetComponent<Renderer>().enabled = false;
            } else
            {
                //planes[i].GetComponent<Renderer>().enabled = true;
            }

            //Now, rotate them so they are facing the camera.
            planes[i].transform.rotation = transform.rotation;
            planes[i].transform.Rotate(270, 0, 0);

            //Finally, scale the planes so they take up the entire viewport.
            Camera cam = GetComponent<Camera>();
    
            Vector3 worldMin = cam.ScreenToWorldPoint(new Vector3(0, 0, dist));
            Vector3 worldMax = cam.ScreenToWorldPoint(new Vector3(0, 0, dist));



            float localWidth = worldMax.x - worldMin.x;
            float localHeight = worldMax.y - worldMin.y;

           // float scaleY = localHeight / planes[i].transform.localScale.y;
            //float scaleX = localWidth / planes[i].transform.localScale.x;

            float scale = 15f;

           // if(scaleY < scaleX)
           // {
            //    scale = scaleX;
            //}

            planes[i].transform.localScale = new Vector3(scale, scale, scale);

            //Set the alpha value based on what we've calculated.
            float alpha = Mathf.Clamp((transform.position - planes[i].transform.position).magnitude/maxDistance, 0f, 0.4f);


            applyAlpha(planes[i], alpha);

        }

    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

}
