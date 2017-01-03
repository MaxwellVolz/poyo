using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    GameController gc;
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            if(!gc.gameEnding)
                gc.gameOver();
        }            
    }    
}
