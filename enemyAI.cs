using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {
    public float movespeed = 5.0f;   
    public Vector2 direction = Vector2.left;
    public bool canKill = true;
    public bool killable = true;
    //public bool     
    Rigidbody2D rb;
    GameController gc;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	void FixedUpdate ()
    {        
        rb.velocity = new Vector2(movespeed * direction.x,rb.velocity.y);        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "clip")
        {
            if (direction == Vector2.left)
            {
                direction = Vector2.right;
                sr.flipX = true;
            }
            else
            {
                direction = Vector2.left;
                sr.flipX = false;
            }
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.collider.name);
        if(col.collider.name.Contains("bullet") && killable)
        {
            Die();
            GameObject.Destroy(col.gameObject);
        }
        if (col.collider.name.Contains("Character") && canKill)
        {
            if(!gc.gameEnding) gc.gameOver();
        }
    }

    void Die()
    {
        GameObject.Destroy(gameObject);
        //add death anim/effects here        
    }
}
