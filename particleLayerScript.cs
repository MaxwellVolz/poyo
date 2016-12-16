using UnityEngine;
using System.Collections;

public class particleLayerScript : MonoBehaviour {


	bool cloudTouch = false;
	public Transform cloudCheck;
	float cloudRadius = 0.5f;
	public LayerMask whatIsPlayer;

	// Use this for initialization
	void Start ()
	{
		// Set the sorting layer of the particle system.
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "foreground";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	}

	// Update is called once per frame
	void FixedUpdate () {


		cloudTouch = Physics2D.OverlapCircle (cloudCheck.position, cloudRadius, whatIsPlayer);
		// Near miss

		if (cloudTouch) {
			Destroy(gameObject);
		}

	}


}
