﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public float maxSpeed = 3.5f;
    public float acceleration = 0.5f;
	bool facingRight = false;
	public float jumpForce = 200;
	public Animator characterAnimator;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.5f;
	public LayerMask whatIsGround;

	public LayerMask whatIsFire;
	public Transform nearMissCheck;
	float nearMissCheckRadius = 2f;
	bool nearMissCollision = false;

	public GameObject bullet;
	public float bulletSpeed = 10f;

	bool inDeadZone = false;
	public LayerMask whatIsDeadZone;

	bool jumpDisabled = false;

	public float airspeedPercent = 0.8f;

	public Rigidbody2D rb;
    public bool isFrozen = false;

	void Start()
	{
		
		// define rigidbody of character component
		rb = GetComponent<Rigidbody2D> ();

	}

	void Update()
	{
		if (grounded && Input.GetButtonDown("Jump") && !isFrozen) {
			Debug.Log ("jumpDisabled");
			Debug.Log (jumpDisabled);
			if (!jumpDisabled) {
				jumpDisabled = true;
				StartCoroutine (Jump ());
			}
		}

		if (grounded && Input.GetButtonDown("Fire2") && !isFrozen) {
			characterAnimator.SetTrigger ("peck");
		}
		//

		if(Input.GetButtonDown("Fire1") && !isFrozen)
        {
			StartCoroutine (FireShot ());

		}

	}

	void FixedUpdate()
	{
        
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		nearMissCollision = Physics2D.OverlapCircle (nearMissCheck.position, nearMissCheckRadius, whatIsFire);
		float move = Input.GetAxis("Horizontal");
		inDeadZone = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsDeadZone);

		if(inDeadZone) Application.LoadLevel (Application.loadedLevelName); 
			

		characterAnimator.SetBool ("ground", grounded);
		characterAnimator.SetFloat ("vSpeed", rb.velocity.y);
		characterAnimator.SetFloat ("speed", Mathf.Abs (move));

		characterAnimator.SetBool ("nearMiss", nearMissCollision);


		// slow mo when moving vertically fast
		/*if(rb.velocity.y > 5 || rb.velocity.y < -2){
			Time.timeScale = 0.6F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		else{
			Time.timeScale = 1F;
			//Time.fixedDeltaTime = 1F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}*/



		if (Input.mousePosition.x > (Screen.width / 2) && !facingRight)
			Flip();
		else if (Input.mousePosition.x < (Screen.width / 2) && facingRight)
			Flip();


        //if (grounded) {
        if (!isFrozen)
        {
            float newVel = rb.velocity.x + (acceleration * move);
            if (Mathf.Abs(newVel) > maxSpeed)
                newVel = maxSpeed * move;

            rb.velocity = new Vector2(newVel, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


		/*} else {
			rb.velocity = new Vector2 (move   * (maxSpeed * airspeedPercent), rb.velocity.y);

		}*/

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator FireShot(){
		

		characterAnimator.SetTrigger ("shoot1");
		yield return new WaitForSeconds (0.5f);
		//float newX = transform.position.x;
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = target - new Vector2 (transform.position.x, transform.position.y);
        direction.Normalize();
        /*float calculatedBulletSpeed = bulletSpeedX;
		if (!facingRight) {
			//bulletSpeedX = -bulletSpeedX;
			calculatedBulletSpeed = -calculatedBulletSpeed;
			newX -= 1f;
		} else {
			newX += 1f;
		}*/

        GameObject bulletClone = (GameObject) Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
		Debug.Log (transform.forward);


        bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
	}

	IEnumerator Jump()
	{
		//Debug.Log ("jumped and waiting to jump again");



		//Time.fixedDeltaTime = 0.7F;

		characterAnimator.SetBool ("ground", false);

		rb.AddForce (new Vector2 (0, jumpForce));

		yield return new WaitForSeconds (0.125f);

		jumpDisabled = false;

		//Debug.Log ("ready to jump again");




	}


}
