using UnityEngine;
using System.Collections;

public class TriggerBoulderTrap : MonoBehaviour {


	public Rigidbody2D theBoulder;
	private bool triggered = false;

	// Use this for initialization
	void Start () {
		theBoulder.constraints = RigidbodyConstraints2D.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log("triggered" + POI);
		if (!triggered && col.name == "Character")
		{
			theBoulder.constraints = RigidbodyConstraints2D.None;
			triggered = true;
		}   
	}
}
