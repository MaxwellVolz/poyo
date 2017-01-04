using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    GameObject player;
    PlayerScript ps;
    Canvas canvas;
    new Camera camera;

	public bool gameEnding = false;
    private float fadeAmount = 1.0f;
    private List<GameObject> colorObjects = new List<GameObject>();
    private float colorDistance = 4.5f;
    private float colorDistanceOffset = 1.0f;
    private string lastScene;
    
    public bool debugging = true;
    
    static Vector3 currentCheckpoint;
    //Save variables
    public int totalCurrency;
    public PowerUps powers;
    public int deaths;
    public List<int> collectedCurrency = new List<int>();
    public List<String> levelsCompleted = new List<String>();
    // Use this for initialization
    void Start ()
    {      
               
            
    }

    void OnGUI()
    {
        if (debugging)
        {
            GUI.Label(new Rect(10, 10, 100, 30), "Money: " + totalCurrency);
            GUI.Label(new Rect(10, 20, 300, 30), "Powers: " + powers);
            GUI.Label(new Rect(10, 30, 300, 30), "Completed Lvls: " + levelsCompleted.Count);
            GUI.Label(new Rect(10, 40, 300, 30), "Deaths: " + deaths);
            GUI.Label(new Rect(Screen.width - 100, 10, 100, 30), "FPS: " + Mathf.Floor(1.0f / Time.deltaTime));

            if (GUI.Button(new Rect(10, 100, 100, 30), "Reset Save"))
            {
                File.Delete(Application.persistentDataPath + "/saveData.dat");
            }
        }

    }

    void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("scene loaded: " + scene.name);
        //If we are in the pre-loading scene, switch to first in build order
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("initial scene loaded, going to first scene in build order");
            SceneManager.LoadScene(1);
        }
            

        if (scene.name != lastScene)
        {
            //loaded a new scene, reset savedata
            currentCheckpoint = Vector3.zero;
            //Debug.Log("restarted scene");
        }


        //clear object references
        colorObjects.Clear();
        player = null;
        fadeAmount = 1.0f;

        
        if (gameEnding)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            gameEnding = false;
        }

        GameObject[] temp = GameObject.FindGameObjectsWithTag("colorable");
        foreach (GameObject obj in temp)
            colorObjects.Add(obj);

        //Setup references        
        player = GameObject.Find("Character");
        if (player)
        {
            ps = player.GetComponent<PlayerScript>();
            camera = Camera.main;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        //checkpoint data found
        if (currentCheckpoint != Vector3.zero)
        {
            player.transform.position = currentCheckpoint;
            camera.transform.position = new Vector3(currentCheckpoint.x, currentCheckpoint.y, camera.transform.position.z);
            //Debug.Log("setting player to: " + currentCheckpoint);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneChanged;

        //give some powerups for testing
        powers = (PowerUps.Bullet | PowerUps.DoubleJump | PowerUps.Glide);
        Load();
        
        //Create save file if none exist
        if (!File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            File.Create(Application.persistentDataPath + "/saveData.dat");
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }    

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Submit"))
        {            
            restartAtCheckpoint();
        }
        if(colorObjects.Count > 0) colorScene();        
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
            }   
        }        
    }

    //Restart current scene
    public void restartLevel()
    {
        //Debug.Log("restarting scene");   
        changeLevel(SceneManager.GetActiveScene().name);              
    }
    
    public void restartAtCheckpoint()
    {
        restartLevel();
        //Debug.Log("restarting level at: " + currentCheckpoint);        
    }    
    
    public void changeLevel(string levelName)
    {
        //add victory stuff here, score or whatever
        lastScene = SceneManager.GetActiveScene().name;
        Debug.Log("changing level to: " +  levelName);
        SceneManager.LoadScene(levelName);        
    }

    public void gameOver()
    {        
        //Debug.Log(canvas.GetComponent<Graphic>().mainTexture);
        gameEnding = true;
        ps.die();
        //Destroy(player);
    }

    public void setCheckpoint(Vector3 checkpoint)
    {
        currentCheckpoint = checkpoint;        
        //Debug.Log("checkpoint set: " + saveData.checkpoint.name);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);

        PlayerData data = new PlayerData();
        data.totalCurrency = totalCurrency;
        data.powers = powers;
        data.deaths = deaths;
        data.collectedCurrency = collectedCurrency;
        data.levelsCompleted = levelsCompleted;

        bf.Serialize(file, data);
        file.Close();
    }
    
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            totalCurrency = data.totalCurrency;
            powers = data.powers;
            deaths = data.deaths;
            collectedCurrency = data.collectedCurrency;
            levelsCompleted = data.levelsCompleted;
        }
    }    
}

[Serializable]
class PlayerData
{
    public int totalCurrency;
    public PowerUps powers;
    public int deaths;
    public List<int> collectedCurrency = new List<int>();
    public List<String> levelsCompleted = new List<String>();    
}

[Flags]
public enum PowerUps
{
    Bullet = 1,
    DoubleJump = 2,
    GroundPound = 4,
    Glide = 8,
    Bomb = 16,
    WaterBalloon = 32
}