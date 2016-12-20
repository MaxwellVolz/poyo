using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

    GameController gc;
    public string nextScene;
    void Start()
    {
        gc = Camera.main.GetComponent<GameController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            gc.changeLevel(nextScene);
        }
    }
}
