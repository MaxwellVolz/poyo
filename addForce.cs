using UnityEngine;
using System.Collections;

public class addForce : MonoBehaviour {

    public Vector2 Force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Character" || other.name == "bullet")
        {            
            other.GetComponent<Rigidbody2D>().AddForce(Force);
        }
    }

}
