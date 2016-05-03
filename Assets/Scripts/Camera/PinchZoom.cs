using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.1f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.1f;        // The rate of change of the orthographic size in orthographic mode.

    public float maxDistance = 40;
    public float minDistance = 5;
    public float distance = 15.0f;

    public Transform target;

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
           
            Camera camera = Camera.main;

            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            { 

                float deltaNorm = deltaMagnitudeDiff > 0? 1.0f : -1.0f;

                float newDist = distance + (transform.forward * deltaMagnitudeDiff).magnitude * (deltaNorm * Time.deltaTime);
                //transform.localPosition = new Vector3(camera.transform.position.x, camera.transform.position.y, distance);
                //transform.Translate(0, 0, newDist-distance);
                
                if(newDist - distance > 0)
                {
                    if(distance >= maxDistance)
                    {
                        return;
                    }
                }
                if(newDist - distance < 0)
                {
                    if(distance <= minDistance)
                    {
                        return;
                    }
                }


                transform.position += transform.forward * deltaMagnitudeDiff * Time.deltaTime;
                distance = newDist;
                //Debug.Log("Mag: " + deltaMagnitudeDiff);
               // Debug.Log("dist: " + distance);

            }
        }
    }
}