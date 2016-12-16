using UnityEngine;
using System.Collections;

public class NearMissScript : MonoBehaviour {

	bool nearMissCollision = false;
	public Transform nearMissCheck;
	float nearMissCheckRadius = 2f;
	public LayerMask whatIsFire;
	public Animator characterAnimator;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Near miss

		nearMissCollision = Physics2D.OverlapCircle (nearMissCheck.position, nearMissCheckRadius, whatIsFire);
		characterAnimator.SetBool ("nearMiss", nearMissCollision);
	
	}
}
