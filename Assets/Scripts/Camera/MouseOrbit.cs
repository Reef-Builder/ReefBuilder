using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbit : MonoBehaviour
{
    public Transform hiddenTarget;
    public Transform target;

    private bool fullControl = false;
    private Vector3 fullControlViewAngle = new Vector3(90, -45, 0);
    private float fullControlDistance = 10;
    private Transform fullControlTarget;

    public float distance = 15.0f;
    
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 5f;
    public float distanceMax = 40f;

    private Rigidbody rigidbody;

	float x = 0.0f;
    float y = 0.0f;

    private List<float> xDeltas;
    private List<float> yDeltas;

    private float minDistanceAboveTerrain = 1;

    private bool newTarget = false;
    private float distToNewTarget;

    private float flySpeed = 10;
    private float lookSpeed = 5;

    //locks the camera, e.g if we're placing a coral.
    private bool locked;

    // Use this for initialization
    void Start()
    {
        xDeltas = new List<float>(5);
        yDeltas = new List<float>(5);

        for (int i = 0; i < 5; i++)
        {
            xDeltas.Add(0);
            yDeltas.Add(0);
        }

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Rigidbody rb = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
        hiddenTarget = new GameObject().transform;
        hiddenTarget.position = target.position;
       // newTarget = true;
        Orbit(hiddenTarget);
    }

    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            fullControl = false;
            if (target.tag.Equals("Controllable"))
            {
                target.GetComponent<FullControl>().active = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            flySpeed = 10;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            flySpeed = 100;
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            flySpeed = -1;
        }

        if (fullControl) {

            Vector3 targetPos = fullControlTarget.position;
            fullControlTarget.LookAt(target);
            Quaternion targetRot = fullControlTarget.rotation;

            Vector3 newPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
            Vector3 newEuler = targetRot.eulerAngles;

            x = Mathf.LerpAngle(x, newEuler.y, Time.deltaTime);
            y = Mathf.LerpAngle(y, newEuler.x, Time.deltaTime);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = newPos;

            float newDist = (target.position - fullControlTarget.position).magnitude;

            hiddenTarget.position = target.position;
            distance = (transform.position - hiddenTarget.position).magnitude;

            if ((targetPos - transform.position).magnitude < 2.0f) {
               // fullControl = false;
            }
            return;
        }

        if (newTarget){
            
            if(flySpeed <= 0){
                hiddenTarget.position = target.position;
            }

            float step = ((hiddenTarget.position - target.position).magnitude / distToNewTarget);
   
            hiddenTarget.position = Vector3.MoveTowards(hiddenTarget.position, target.position, Mathf.SmoothStep(flySpeed * 0.7f * Time.deltaTime, flySpeed * Time.deltaTime, step));

            if ((hiddenTarget.position - target.position).magnitude <= 0.1f)
            {
                newTarget = false;
            }

          //  return;
        } else
        {
            hiddenTarget.position = target.position;
        }

		if (locked && !newTarget)
        {
            return;
        }

        if (Application.isMobilePlatform)
        {
            Orbit(hiddenTarget);
        }
        else
        {
            OrbitMouse(hiddenTarget);
        }


    }

    private void OrbitMouse(Transform target)
    {
		if (target)
        {

            float lastX = x;
            float lastY = y;
            float lastDistance = distance;

            if (!fullControl && !locked && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.5f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.5f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                 //distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
      
            if (isTooCloseToObject(position) && position.y < transform.position.y)
            {
                //Get rid of changes if we're too close to terrain.
                x = lastX;
                y = lastY;
                distance = lastDistance;
                return;
            }

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private void Orbit(Transform target)
    {
        if (target)
        {

            float lastX = x;
            float lastY = y;
            float lastDistance = distance;

            Quaternion rotation = transform.rotation;

            if (!fullControl && !locked && Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject())
            {

                xDeltas.Add(Input.GetTouch(0).deltaPosition.x * xSpeed * distance * Time.deltaTime);
                yDeltas.Add(Input.GetTouch(0).deltaPosition.y * ySpeed * Time.deltaTime);

                xDeltas.RemoveAt(0);
                yDeltas.RemoveAt(0);

                x += mean(xDeltas);
                y -= mean(yDeltas);

                y = ClampAngle(y, yMinLimit, yMaxLimit);
                rotation = Quaternion.Euler(y, x, 0);
            }

            distance = GetComponent<PinchZoom>().distance;

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
               // distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            if (isTooCloseToObject(position) && position.y < transform.position.y)
            {
                //Get rid of changes if we're too close to terrain.
                x = lastX;
                y = lastY;
                distance = lastDistance;
                return;
            }

            transform.rotation = rotation;
            transform.position = position;

        }
    }

    public void FlyTo(Transform target){

       

        this.target = target;
        newTarget = true;
        distToNewTarget = (target.position - hiddenTarget.position).magnitude;
       // newPosition = target.position;
       // lookPosition = transform.forward * distance;
    }

    private bool isTooCloseToObject(Vector3 position)
    {
        float min = minDistanceAboveTerrain;

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit))
        {
            if(hit.distance < min)
            {
                return true;
            }
        }

        return false;

    }

    public static float mean(List<float> values)
    {
        float sum = 0;
        for (int i = 0; i < values.Count; i++)
        {
            sum += values[i];
        }
        float count = values.Count;
        return sum / count;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void lockCamera(bool locked)
    {
        this.locked = locked;
    }

    /** Sets the camera to full control mode.  This locks the camera at a specific rotation from the target.  Pressing escape will turn this off. */
    public void setFullControl(bool fullControl) {
        this.fullControl = fullControl;
        if (fullControl == true) {
            fullControlTarget = target.FindChild("Body").FindChild("follow");
        }
    }

}