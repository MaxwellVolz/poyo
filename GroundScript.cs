using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {
	public Transform player;
	public Vector3 offset;
	float vSpeed = 0f;
	//public Camera mainCamera;


	// using animator influence camera distance
	public Animator characterAnimator;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//vSpeed = characterAnimator.GetFloat ("vSpeed");

		// camera follow based on vertical speed
		//Camera.main.orthographicSize = (Camera.main.orthographicSize + zoom + (Mathf.Abs(characterAnimator.GetFloat ("vSpeed")) / 8))/2;

		// camera follow distance based on vertical position
		// Camera.main.orthographicSize = (Camera.main.orthographicSize + zoom + (Mathf.Abs(player.position.y) / 4))/2;



		transform.position = new Vector3 ( player.position.x  + offset.x, -10.0f,offset.z);
	}
}
