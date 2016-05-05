using UnityEngine;
using System.Collections;


/**

    A script for adding test coral to the scene without
    having to use a UI.  Add this script to a controller object in 
    the scene.

    Press 'C' to add a coral.  The coral will attempt to be placed when 'C' 
    is released.  This mimics what will be done with the UI, where it will
    be the mouse instead of a hotkey.  As soon as 'C' is pressed, a coral prefab will
    be instantiated and follow the mouse.

*/
public class HotkeyObjectCreate : MonoBehaviour {

    private bool down = false;

    public GameObject terrain;

    private Transform coral;
    public Transform prefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!down && Input.GetKeyDown(KeyCode.C))
        {
            down = true;
            coral = (Transform)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            coral.GetComponent<SnapToTerrain>().terrain = terrain;

            Camera.main.GetComponent<MouseOrbit>().lockCamera(true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            down = false;
            bool placed = coral.GetComponent<SnapToTerrain>().SetLocked(true);
            if (!placed)
            {
                Destroy(coral.gameObject);
            }
            coral = null;
            Camera.main.GetComponent<MouseOrbit>().lockCamera(false);
        }

    }
}
