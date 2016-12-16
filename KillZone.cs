using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    public GameController gc;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            gc.gameOver();
        }            
    }    
}
