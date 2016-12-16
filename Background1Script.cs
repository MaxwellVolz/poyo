using UnityEngine;
using System.Collections;

public class Background1Script : MonoBehaviour {


	// THIS SCRIPT IS NOT WORKING DO NOT ENABLE UNLESS YOU FIX IT
	//
	// ATTEMPT AT BACKGROUND SCROLLING BASED ON CHARACTER POSITION

	public Transform bg1;
	public Transform player;

	public float startingX = -0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var playerPos = player.position.x / 5;
		var runningX = startingX;
		//Debug.Log (playerPos);
		runningX += playerPos / 5;
		Debug.Log ("runningX:" + runningX + " bg1.position.y: " + bg1.position.y);
		//.Log (startingX);
		//bg1.position.x = runningX;
		bg1.position = new Vector3 ( runningX , 7 , 0);
	}
}
