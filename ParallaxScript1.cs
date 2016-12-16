using UnityEngine;
using System.Collections;

public class ParallaxScript1 : MonoBehaviour {

	public int speed = 115;
	public Transform player;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 ( (player.position.x  + offset.x)/(speed/100f),transform.position.y + offset.y,transform.position.z + offset.z);
		//transform.Translate((Vector3.right * Time.deltaTime)/speed, Camera.main.transform);
	}
}
