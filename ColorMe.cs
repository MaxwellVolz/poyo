using UnityEngine;
using System.Collections;

public class ColorMe : MonoBehaviour {

	public LayerMask whoIsCharacter;
	public Transform thisObject;
	private Transform characterObject;

	private float distanceToCharacter;

	bool touched = false;
	bool closelyTouched = false;
	bool beenTouched = false;

	private SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = gameObject.GetComponent<SpriteRenderer> ();
		characterObject = GameObject.Find ("Character").transform;
		distanceToCharacter = (thisObject.position - characterObject.position).magnitude;
	}

	// Using FixedUpdate() CRUSHES FRAMES
	void Update()
	{
		distanceToCharacter = (thisObject.position - characterObject.position).magnitude;
		//Debug.Log ("distanceToCharacter: " + distanceToCharacter);
		if (!beenTouched && (distanceToCharacter < 20)) {
			Color (distanceToCharacter);
		}
	}
		
	void Color(float d){

		if (d < 2) {
			myRenderer.color = new Color(1,1,1,1);
			beenTouched = true;
		} else {
			float colorNumber = (Mathf.Clamp (d*5, 0, 100) / -100) + 1.2f ;
			myRenderer.color = new Color (colorNumber, colorNumber, colorNumber, 1);
		}
	}


}
