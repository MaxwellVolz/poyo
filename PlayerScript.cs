using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public float maxSpeed = 3.5f;
    public float acceleration = 0.5f;
	bool facingRight = false;
	public float jumpForce = 200;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.5f;
	public LayerMask whatIsGround;
/*
	public LayerMask whatIsFire;
	public Transform nearMissCheck;
	float nearMissCheckRadius = 2f;
	bool nearMissCollision = false;
    */

	public GameObject bullet;
	public float bulletBaseSpeed = 5f;
    public float bulletMaxSpeed = 20f;
    public float fireRate = 0.5f;
    public float bulletChargeRate = 0.25f;
    public float bulletLifetime = 1.0f;
    private float m_lastShot;
    private float bulletPower;

	bool jumpDisabled = false;

	public float airspeedPercent = 0.8f;

	Rigidbody2D rb;
	public Animator bodyAnimController;
	public Animator headAnimController;
    public bool isFrozen = false;

	void Start()
	{
		
		// define rigidbody of character component
		rb = GetComponent<Rigidbody2D> ();

	}

	void Update()
	{
		if (grounded && Input.GetButtonDown("Jump") && !isFrozen && !jumpDisabled) {
			if (!jumpDisabled) {
				jumpDisabled = true;
				Jump ();
			}
		}

		if (grounded && Input.GetButtonDown("Fire1") && !isFrozen)
        {
            bulletPower = bulletBaseSpeed;
            ChargeShot();
        }

        if (Input.GetButton("Fire1") && !isFrozen)
        {
            ChargeShot();
        }

		if(Input.GetButtonUp("Fire1") && !isFrozen)
        {
            FireShot(bulletPower);            
		}

	}

	void FixedUpdate()
	{
        
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis("Horizontal");			


		bodyAnimController.SetBool ("ground", grounded);
		bodyAnimController.SetFloat ("vSpeed", rb.velocity.y);
		bodyAnimController.SetFloat ("speed", Mathf.Abs (move));


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
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FireShot(float force){

        if (Time.time >= m_lastShot + fireRate)
        {
			headAnimController.SetBool("chargingShot",false);
            //float newX = transform.position.x;
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = target - new Vector2(transform.position.x, transform.position.y);
            direction.Normalize();

            GameObject bulletClone = (GameObject)Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * force;
            m_lastShot = Time.time;
            bulletPower = bulletBaseSpeed;
            Destroy(bulletClone, bulletLifetime + (force * 0.1f));
            Debug.Log(force);
        }
	}

    void ChargeShot()
    {
        if (Time.time >= m_lastShot + fireRate)
        {
			headAnimController.SetBool("chargingShot",true);
            if (bulletPower < bulletMaxSpeed)
            {
                bulletPower += bulletChargeRate;
                Debug.Log(bulletPower);
            }
        }

    }

    public void die()
    {
        isFrozen = true;

        //death animation here
    }

	void Jump()
	{
		//Debug.Log ("jumped and waiting to jump again");



		//Time.fixedDeltaTime = 0.7F;

		bodyAnimController.SetBool ("ground", false);

		rb.AddForce (new Vector2 (0, jumpForce));

		jumpDisabled = false;

		//Debug.Log ("ready to jump again");




	}


}
