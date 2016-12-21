using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {

    GameController gc;
    public Vector3 spawnPoint;
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            if (spawnPoint == Vector3.zero) gc.setCheckpoint(this.gameObject.transform.position);
            else gc.setCheckpoint(spawnPoint);
        }
    }
}
