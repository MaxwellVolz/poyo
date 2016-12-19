using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{

    public float dampTime = 0.15f;
    public float boundY = -6.0f;
    public Vector2 boundX;
    private Vector3 velocity = Vector3.zero;
    private Transform target;
    private float cameraZoom = 5.0f; //default zoom for the camera
    private float zoomAmount = 5.0f;
    private float m_zoomSpeed = 1.0f;    
    private Transform oldTarget;
    private float temp;
    private float oldDamp;
    private bool reset;
    private int zooming; // 0 = no zoom, 1 = zoom out, 2 = zoom in
    new Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
        cameraZoom = camera.orthographicSize;
        target = GameObject.Find("Character").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            //Adjust bounds to zoom level
            float offset = 0;
            if(camera.orthographicSize != cameraZoom)
            {
                offset = cameraZoom - camera.orthographicSize;
                //Debug.Log(boundY - offset);                
            }
                if (destination.y < (boundY - offset))
                    destination.y = (boundY - offset);
                if (destination.x < boundX.x) //todo: add offset for x coords
                    destination.x = boundX.x;
                else if (destination.x > boundX.y)
                    destination.x = boundX.y;
            
                       
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            if(reset == true && Mathf.Abs(transform.position.x - destination.x) < 1.5)
            {
                target.GetComponent<PlayerScript>().isFrozen = false;
                //Debug.Log("unfrozen");
                dampTime = oldDamp;
                reset = false;
            }
            switch (zooming)
            {
                case 0:
                    break;
                case 1:
                    if (camera.orthographicSize < zoomAmount)
                    {
                        camera.orthographicSize += m_zoomSpeed;
                        if (camera.orthographicSize > zoomAmount)
                        {
                            camera.orthographicSize = zoomAmount;
                            zooming = 0;
                        }
                    }
                    break;
                case 2:
                    if (camera.orthographicSize > zoomAmount)
                    {
                        camera.orthographicSize -= m_zoomSpeed;
                        if (camera.orthographicSize < zoomAmount)
                        {
                            camera.orthographicSize = zoomAmount;
                            zooming = 0;
                        }
                    }
                    break;
                    /* if (camera.orthographicSize < zoomAmount)
                     {
                         camera.orthographicSize += (zoomSpeed * 0.01f);
                         if (camera.orthographicSize > zoomAmount)
                         {
                             camera.orthographicSize = zoomAmount;
                         }
                     }
                     */
            }
            //Debug.Log(destination.x - transform.position.x);
        }
        if (oldTarget && Time.time >= temp)
        {
            dampTime /= 2;
            target = oldTarget;
            oldTarget = null;
            reset = true;
            
            float curZoom = camera.orthographicSize;
            if (curZoom < cameraZoom)
            {
                zooming = 1;
                zoomAmount = cameraZoom;
            }
            if(curZoom > cameraZoom)
            {
                zooming = 2;
                zoomAmount = cameraZoom;
            }
        }     
        
    }    

    public void LookAtPOI(Transform POI, float time, float dampening = .15f, float zoom = 5.0f, float zoomSpeed = 1.0f)
    {
        /* Vector3 point = camera.WorldToViewportPoint(target.position);
         Vector3 delta = POI.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
         Vector3 destination = transform.position + delta;

         transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampening);*/
        target.GetComponent<PlayerScript>().isFrozen = true;
        if(zoom > 5.0f)
        {
            zooming = 1;
            zoomAmount = zoom;
        }
        else if(zoom < 5.0f)
        {
            zooming = 2;
            zoomAmount = zoom;
        }
        m_zoomSpeed = zoomSpeed;      
        oldTarget = target;
        target = POI;
        temp = Time.time + time;
        oldDamp = dampTime;
        dampTime = dampening;
    }
}