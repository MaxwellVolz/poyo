using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour {

	public Transform shadow;
	public Transform shadowCheck;
	private float shadowStartY;
	private int layerMaskInt = 1 << 8;

	// Use this for initialization
	void Start () {
		shadowStartY = shadow.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//shadow.position = hit.point;

		//Debug.Log ("raycast result " + hit.point);
		//Debug.Log ("raycast result what" + hit.collider);

		// shadow controls

		RaycastHit2D hit = Physics2D.Raycast(shadowCheck.position,Vector2.down,3000.0f,layerMaskInt);

		shadow.position = new Vector2(shadow.position.x,hit.point.y);
	}
}
