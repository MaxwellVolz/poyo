using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {

    public SmoothCamera2D cameraScript;
    public Transform POI;
    public float time;
    public float dampening;
    public float zoom = 5.0f;
    private bool triggered = false;
    public float zoomSpeed = 1.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("triggered" + POI);
        if (!triggered && col.name == "Character")
        {
            cameraScript.LookAtPOI(POI, time, dampening, zoom, zoomSpeed);
            triggered = true;
        }   
    }
}
