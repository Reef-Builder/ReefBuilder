using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class StoreChest : MonoBehaviour
{

    public GameObject top;
    public GameObject particles;

    public GameObject UI;

    public ParticleSystem sceneTransitionParticles;

    private Vector3 startTopRotation;
    private Vector3 endTopRotation;

    public GameObject FirstMenuObject;

    public Transform CameraMainTransform;
    public Transform CameraStoreTransform;

    public Canvas canvas;

    private float oldCameraFogDensity;
    private float cameraFogDensity = 0.23f;

    private bool storeOpen = false;
    private bool closingStore = false;
    private bool openingStore = false;

    // Use this for initialization
    void Start()
    {
        startTopRotation = top.transform.localRotation.eulerAngles;
        endTopRotation = new Vector3(-222, startTopRotation.y, startTopRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(!closingStore && !openingStore))
        {
            return;
        }

        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (storeOpen)
                    {
                        CloseStore();
                    }
                    else
                    {
                        OpenStore(null);
                    }
                }
            }
        }
    }


    void OnMouseDown()
    {
        if(!(!closingStore && !openingStore)){
            return;
        }

        if (storeOpen)
        {
            CloseStore();
        }
        else
        {
            OpenStore(null);
        }
    }

    public void CloseStore()
    {
        var euler = top.transform.localRotation.eulerAngles;

        startTopRotation = euler;

        endTopRotation = new Vector3(-90, euler.y, euler.z);

        storeOpen = false;

        StartCoroutine("DoCloseStoreCoroutines");

        particles.SetActive(false);
    }

    private IEnumerator DoCloseStoreCoroutines()
    {
        StartCoroutine("CloseLid");
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("MoveCameraAwayFromStore");
    }

    private IEnumerator MoveCameraAwayFromStore()
    {
        var duration = 1f;
        var startTime = Time.time;

        var fog = Camera.main.GetComponent<GlobalFog>();

        fog.heightDensity = oldCameraFogDensity;

        UI.SetActive(true);

        while (Time.time - startTime < duration)
        {
            var startRotation = Camera.main.transform.eulerAngles;
            var endRotation = CameraMainTransform.eulerAngles;

            var startPosition = Camera.main.transform.position;
            var endPosition = CameraMainTransform.position;

            Camera.main.transform.localRotation = Quaternion.Slerp(
                                                  Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z),
                                                  Quaternion.Euler(endRotation.x, endRotation.y, endRotation.z),
                                                  (Time.time - startTime) / duration);

            Camera.main.transform.position = Vector3.Slerp(
                                             startPosition,
                                             endPosition,
                                             (Time.time - startTime) / duration);

            yield return null;
        }
    }

    private IEnumerator CloseLid()
    {
        closingStore = true;
        var startTime = Time.time;
        var duration = 0.4f;

        //sceneTransitionParticles.Play();

        while (Time.time - startTime < duration)
        {
            top.transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(startTopRotation.x, startTopRotation.y, startTopRotation.z),
                Quaternion.Euler(endTopRotation.x, endTopRotation.y, endTopRotation.z),
                (Time.time - startTime) / duration);

            yield return null;
        }

        top.transform.localRotation = Quaternion.Euler(endTopRotation);
        closingStore = false;
    }

    public void OpenStore(GameObject callerButton)
    {
        callerButton?.SetActive(false);
        particles.SetActive(true);
        UI.SetActive(false);

        startTopRotation = top.transform.localRotation.eulerAngles;
        endTopRotation = new Vector3(-222, startTopRotation.y, startTopRotation.z);

        storeOpen = true;

        StartCoroutine("DoOpenStoreCoroutines");
    }

    private IEnumerator DoOpenStoreCoroutines()
    {
        StartCoroutine("OpenLid");
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("MoveCameraToStore");
    }

    private IEnumerator MoveCameraToStore()
    {
        var duration = 1f;
        var startTime = Time.time;

        var fog = Camera.main.GetComponent<GlobalFog>();

        oldCameraFogDensity = fog.heightDensity;
        fog.heightDensity = cameraFogDensity;

        while (Time.time - startTime < duration)
        {
            var startRotation = Camera.main.transform.eulerAngles;
            var endRotation = CameraStoreTransform.eulerAngles;

            var startPosition = Camera.main.transform.position;
            var endPosition = CameraStoreTransform.position;

            Camera.main.transform.localRotation = Quaternion.Slerp(
                                                  Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z),
                                                  Quaternion.Euler(endRotation.x, endRotation.y, endRotation.z),
                                                  (Time.time - startTime) / duration);

            Camera.main.transform.position = Vector3.Slerp(
                                             startPosition,
                                             endPosition,
                                             (Time.time - startTime) / duration);

            yield return null;
        }
    }

    private IEnumerator OpenLid()
    {
        openingStore = true;

        var startTime = Time.time;
        var duration = 0.4f;

        sceneTransitionParticles.Play();

        while (Time.time - startTime < duration)
        {
            top.transform.localRotation = Quaternion.Lerp(
                Quaternion.Euler(startTopRotation.x, startTopRotation.y, startTopRotation.z),
                Quaternion.Euler(endTopRotation.x, endTopRotation.y, endTopRotation.z),
                (Time.time - startTime) / duration);

            yield return null;
        }

        top.transform.localRotation = Quaternion.Euler(endTopRotation);
        openingStore = false;
    }
}
