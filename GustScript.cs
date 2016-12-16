using UnityEngine;
using System.Collections;

public class GustScript : MonoBehaviour {

	public Transform groundCheck;
	float groundRadius = 0.5f;
	public LayerMask whatIsGround;
	public Rigidbody2D rb;

	bool gustRight = false;
	public LayerMask whatIsGustRight;

	bool gustUp = false;
	public LayerMask whatIsGustUp;

	bool gustLeft = false;
	public LayerMask whatIsGustLeft;

	bool gustDown = false;
	public LayerMask whatIsGustDown;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gustRight = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGustRight);
		gustUp = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGustUp);
		gustLeft = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGustLeft);
		gustDown = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGustDown);

		// force of wind stuff
		if (gustRight) {
			Debug.Log ("Attempting to addforce");
			rb.AddForce (Vector2.right * 300);
			rb.AddForce (Vector2.up * 20);
		}
		if (gustUp) {
			Debug.Log ("Attempting to addforce");
			//rb.AddForce (Vector2.right * 300);
			rb.AddForce (Vector2.up * 300);
		}
		if (gustLeft) {
			Debug.Log ("Attempting to addforce");
			rb.AddForce (Vector2.left * 300);
			rb.AddForce (Vector2.up * 20);
		}
		if (gustDown) {
			Debug.Log ("Attempting to addforce");
			rb.AddForce (Vector2.down * 50);
		}
	}
}
