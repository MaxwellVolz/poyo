using UnityEngine;
using System.Collections;

public class RockTowerTrigger : MonoBehaviour {

	bool triggerTouch = false;
	public Transform triggerCheck;
	float triggerRadius = 0.5f;	
	public LayerMask whatIsTrigger;

	// Use this for initialization
	void Start ()
	{
		// Set the sorting layer of the particle system.
		//GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "foreground";
		//GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	}

	// Update is called once per frame
	void FixedUpdate () {


		triggerTouch = Physics2D.OverlapCircle (triggerCheck.position, triggerRadius, whatIsTrigger);
		// Near miss

		if (triggerTouch) {
			Debug.Log ("DROP THE ROCK");

			HingeJoint2D hinge = GetComponent<HingeJoint2D> ();
			JointAngleLimits2D limits = hinge.limits;
			Debug.Log (limits.min);

			limits.min = -12;

			hinge.limits = limits;
			hinge.useLimits = true;
			//Destroy(gameObject);
		}

	}


}
