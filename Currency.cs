using UnityEngine;
using System.Collections;
using System;

public class Currency : MonoBehaviour {
    
    public int UID; //This needs to be set for each currency object and MUST be unique    

    GameController gc;    
    
    void Start () {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        //Check if this currency object has already been collected, and remove it if true               
        if(gc.collectedCurrency.Contains(UID))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            gc.collectedCurrency.Add(UID);
            gc.totalCurrency += 1;
            GameObject.Destroy(gameObject);
            //todo: add jingle and or particle effects
        }
    }
}
