using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Restart current scene
    public void restartLevel()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
