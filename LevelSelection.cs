using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
    GameController gc;
    
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    void Update()
    {        
        
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log(hit.transform.name);
                gc.changeLevel(hit.transform.name);
            }
        }
    }   
}