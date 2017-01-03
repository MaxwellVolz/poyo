using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public float maxSpeed = 3.5f;
    public float acceleration = 0.5f;
	bool facingRight = false;
	public float jumpForce = 200;
    private PowerUps powers;

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
    private Transform bulletPos;
	
    bool doubleJumped = false;

	public float airspeedPercent = 0.8f;

	Rigidbody2D rb;
	Animator bodyAnimController;
	Animator headAnimController;
    Transform head;
    GameController gc;
    public bool isFrozen = false;
    private float maxRotationDegrees = 50;

	void Start()
	{
		
		// define rigidbody of character component
		rb = GetComponent<Rigidbody2D> ();
        bodyAnimController = transform.Find("characterBody").GetComponent<Animator>();
        head = transform.Find("characterHead");
        headAnimController = head.GetComponent<Animator>();
        bulletPos = head.FindChild("bulletPos").GetComponent<Transform>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        powers = gc.powers;
    }

	void Update()
	{
		if (Input.GetButtonDown("Jump") && !isFrozen)
        {
            //check if we have unlocked doublejump but havent jumped twice yet 
            if (!grounded && ((powers & PowerUps.DoubleJump) == PowerUps.DoubleJump) && !doubleJumped)
            {
                Jump();
                doubleJumped = true;
            }
            else if (grounded)
            {
                Jump();                
            }			
		}

        if((powers & PowerUps.Bullet) == PowerUps.Bullet)
        {
            if (Input.GetButtonDown("Fire1") && !isFrozen)
            {
                bulletPower = bulletBaseSpeed;
                ChargeShot();
            }

            if (Input.GetButton("Fire1") && !isFrozen)
            {
                ChargeShot();
            }

            if (Input.GetButtonUp("Fire1") && !isFrozen)
            {
                FireShot(bulletPower);
            }
        }

	}

	void FixedUpdate()
	{
        
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

        if (grounded)
            doubleJumped = false;

		float move = Input.GetAxis("Horizontal");
        if (isFrozen)
            move = 0;			


		bodyAnimController.SetBool ("ground", grounded);
		bodyAnimController.SetFloat ("vSpeed", rb.velocity.y);
		bodyAnimController.SetFloat ("speed", Mathf.Abs (move));


		if (Input.mousePosition.x > (Screen.width / 2) && !facingRight)
			Flip();
		else if (Input.mousePosition.x < (Screen.width / 2) && facingRight)
			Flip();
        
        float rotation = (Input.mousePosition.y / Screen.height) * 180;
        
        rotation -= 90;
        if (rotation > maxRotationDegrees)
            rotation = maxRotationDegrees;
        if(rotation < -maxRotationDegrees)
            rotation = -maxRotationDegrees;

        if (!facingRight)
        {
            rotation *= -1;
        }
        
        if(!isFrozen) head.eulerAngles = new Vector3(0, 0, rotation);

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
        if (!isFrozen)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
	}

	void FireShot(float force){

        if (Time.time >= m_lastShot + fireRate)
        {
			headAnimController.SetBool("chargingShot",false);
            //float newX = transform.position.x;
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = target - new Vector2(transform.position.x, transform.position.y);
            direction.Normalize();

            GameObject bulletClone = (GameObject)Instantiate(bullet, new Vector2(bulletPos.position.x, bulletPos.position.y), Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * force;
            m_lastShot = Time.time;
            bulletPower = bulletBaseSpeed;
            Destroy(bulletClone, bulletLifetime + (force * 0.1f));
            //Debug.Log(force);
        }
	}

    void ChargeShot()
    {
        if (Time.time >= m_lastShot + fireRate)
        {
			headAnimController.SetBool("chargingShot",true);
            if (bulletPower < bulletMaxSpeed)            
                bulletPower += bulletChargeRate;            
        }

    }

    public void die()
    {
        isFrozen = true;
        rb.constraints = RigidbodyConstraints2D.None;
        gc.deaths += 1;
        //death animation here
    }

	void Jump()
	{
		//Debug.Log ("jumped and waiting to jump again");

		bodyAnimController.SetBool ("ground", false);

		rb.AddForce (new Vector2 (0, jumpForce));
		//Debug.Log ("ready to jump again");
	}

}
