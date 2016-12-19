using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    GameController gc;
    void Start()
    {
        gc = Camera.main.GetComponent<GameController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            gc.gameOver();
        }            
    }    
}
