using UnityEngine;
using System.Collections;

// current use: nothing
public class WallScript : MonoBehaviour {
	
	bool wallCollision = false;
	public Transform wallCheck;
	float wallCheckRadius = 0.5f;
	public LayerMask whatIsWall;
	public Animator characterAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		wallCollision = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		characterAnimator.SetBool ("touchingWall", wallCollision);
	
	}
}
