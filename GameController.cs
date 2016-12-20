using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    GameObject player;
    PlayerScript ps;
    Canvas canvas;
    new Camera camera;

	private bool gameEnding = false;
    private float fadeAmount = 1.0f;
    private List<GameObject> colorObjects = new List<GameObject>();
    private float colorDistance = 4.5f;
    private float colorDistanceOffset = 1.0f;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Character");        
        ps = player.GetComponent<PlayerScript>();
        camera = GetComponent<Camera>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
        //Populate color object list, probably a better way to do this
        GameObject[] temp = GameObject.FindGameObjectsWithTag("colorable");
        foreach (GameObject obj in temp)        
            colorObjects.Add(obj);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {            
            restartLevel();
        }
        colorScene();
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
        
    void colorScene()
    {        
        for (int i = 0;i < colorObjects.Count;i++)
        {
            Vector2 charPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 objectPos = new Vector2(colorObjects[i].transform.position.x, colorObjects[i].transform.position.y);
            float distance = Vector2.Distance(charPos, objectPos);            
            if (distance > colorDistance) //Goto next iteration if object outside minimum color distance
                continue;

            float alpha = (distance - colorDistanceOffset) / colorDistance;
            colorObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), alpha);
            if (alpha <= .15f) //if over 85% colored then make 100% and remove from list
            {
                colorObjects[i].GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), 0);
                colorObjects.RemoveAt(i);
                //Debug.Log("Removing object: " + i);
                continue;
            }
                      
        }        
    }

    //Restart current scene
    public void restartLevel()
    {        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {        
        //Debug.Log(canvas.GetComponent<Graphic>().mainTexture);
        gameEnding = true;
        ps.die();
        //Destroy(player);
    }
}
