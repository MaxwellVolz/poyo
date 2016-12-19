using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    GameObject player;
    PlayerScript ps;
    Canvas canvas;
    new Camera camera;

    private bool gameEnding = false;
    private float fadeAmount = 1.0f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Character");        
        ps = player.GetComponent<PlayerScript>();
        camera = GetComponent<Camera>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
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
            canvas.GetComponent<Graphic>().color = Color.Lerp(Color.white,new Color(1,1,1,0), fadeAmount);
            Time.timeScale = Mathf.Lerp(0.05f, 0.7f, (fadeAmount * .1f));
            Time.fixedDeltaTime = 0.02F * Time.timeScale;            
        }
    }
        


    //Restart current scene
    public void restartLevel()
    {        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameEnding = true;
        ps.die();
        //Destroy(player);
    }
}
