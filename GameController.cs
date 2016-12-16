using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    GameObject player;
    PlayerScript ps;
    public Canvas canvas;
    new Camera camera;

    private bool gameEnding;
    private float fadeAmount = 1.0f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Character");        
        ps = player.GetComponent<PlayerScript>();
        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            restartLevel();
        }
        
    }
    void FixedUpdate()
    {
        if (gameEnding)
        {
            camera.GetComponent<ColorCorrectionCurves>().saturation = Mathf.Lerp(0,.70f,fadeAmount);
            if(fadeAmount > 0)
                fadeAmount -= 0.025f;
            canvas.GetComponent<Graphic>().color = Color.white;
        }
    }
        


    //Restart current scene
    public void restartLevel()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {        
        //Debug.Log(canvas.GetComponent<Graphic>().mainTexture);
        gameEnding = true;
        ps.die();
    }
}
