using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbit : MonoBehaviour
{

    public Transform target;
    public float distance = 15.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 5f;
    public float distanceMax = 40f;

    float x = 0.0f;
    float y = 0.0f;

    private List<float> xDeltas;
    private List<float> yDeltas;

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
        Orbit(target);
    }

    void LateUpdate()
    {

        if (locked)
        {
            return;
        }

        if (Application.isMobilePlatform)
        {
            Orbit(target);
        } else
        {
            OrbitMouse(target);
        }
       
       
    }

    private void OrbitMouse(Transform target)
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
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
               // distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private void Orbit(Transform target)
    {
        if (target)
        {
      
            Quaternion rotation = transform.rotation;

            if (Input.touchCount == 1)
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
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
   
            transform.rotation = rotation;
            transform.position = position;
          
        }
    }
 
    public static float mean(List<float> values)
    {
        float sum = 0;
        for(int i = 0; i < values.Count; i++)
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

}