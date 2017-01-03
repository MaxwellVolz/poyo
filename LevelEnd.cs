using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    GameController gc;
    public string nextScene;
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Character")
        {
            if(!gc.levelsCompleted.Contains(SceneManager.GetActiveScene().name))
                gc.levelsCompleted.Add(SceneManager.GetActiveScene().name);
            gc.changeLevel(nextScene);
        }
    }
}
