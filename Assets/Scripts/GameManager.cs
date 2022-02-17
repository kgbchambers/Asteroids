using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager>
{
    public GameObject playerPref;
    public GameObject player;
    public GameObject largeAsteroidPref;
    public GameObject saucerPref;

    public Image background;
    public GameObject resumeButton;
    public GameObject saveButton;
    public GameObject loadButton;
    public GameObject quitButton;
    public GameObject newButton;


    private List<GameObject> asteroids = new List<GameObject>();
    private List<GameObject> saucers = new List<GameObject>();

    public bool isPaused;
    public bool hitESC;

    private int curScore;
    private int highScore;
    private int curLevel;
    private int lives;

    private Transform spawnLocation;

    [SerializeField]
    private Text curScoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text curLevelText;
    [SerializeField]
    private Text livesText;



    private void Start()
    {
        ResetGame();
        highScore = 0;
        hitESC = false;
        resumeButton.SetActive(false);
        InvokeRepeating("spawnSaucer", 5f, 10f);
    }

    private void Update()
    {


        if (isPaused)
        {
            Time.timeScale = 0f;
            if(hitESC)
                resumeButton.SetActive(true);

            saveButton.SetActive(true);
            loadButton.SetActive(true);
            quitButton.SetActive(true);
            newButton.SetActive(true);
            background.gameObject.SetActive(true);
        }

        if (!isPaused)
        {
            newButton.SetActive(false);
            resumeButton.SetActive(false);
            saveButton.SetActive(false);
            loadButton.SetActive(false);
            quitButton.SetActive(false);
            background.gameObject.SetActive(false);

            Time.timeScale = 1f;
            if(asteroids.Count <= 0)
            {
                curLevel++;
                curLevelText.text = "Level: " + curLevel;
                buildLevel();
            }
        }

    }


    public void setText()
    {
        curScoreText.text = "Score: " + curScore;
        curLevelText.text = "Level: " + curLevel;
        livesText.text = "Lives: " + lives;
        highScoreText.text = "Highscore: " + highScore;
    }


    public void updateScore()
    {
        curScore += 10;
        curScoreText.text = "Score: " + curScore; 
        if(curScore > highScore)
        {
            highScore = curScore;
            highScoreText.text = "Highscore: " + highScore;
        }
    }


    public void loseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            ResetGame();
        }
    }



    public void ResetGame()
    {
        Camera.main.orthographicSize = 12;
        curScore = 0;
        curLevel = 1;
        lives = 3;
        //highScore = 0;
        setText();
        if(!player)
            player = Instantiate(playerPref, Vector3.zero, Quaternion.identity);
        isPaused = true;
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
        foreach (GameObject saucer in saucers)
        {
            Destroy(saucer);
        }
    }


    public void SaveGame()
    {
        //create a save instance with all the data for the current session save into it
        PlayerData save = CreateSaveGameObject();

        //create a binary formatter and filestream passing a path for the save instance to be save to.
        //It will serialize the data (into bytes) and write to the disk and close the filestream.
        //There will then be a file name gamesave.save on the players computer - Note that we can use whatever we want
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);

        //reset the game state
        Debug.Log("Game Saved");
    }



    public void NewGame()
    {
        ResetGame();
        buildLevel();
        isPaused = false;

    }


    public void buildLevel()
    {
        for(int i = 0; i < curLevel + 1; i++)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject asteroidInstance = Instantiate(largeAsteroidPref, spawnPosition, Quaternion.identity);
            asteroids.Add(asteroidInstance);
            Camera.main.orthographicSize += 1;
        }
    }




    private PlayerData CreateSaveGameObject()
    {
        PlayerData save = new PlayerData();

        save.curLevel = curLevel;
        save.curScore = curScore;
        save.highScore = highScore;
        save.lives = lives;

        return save;
    }

    public void LoadGame()
    {
        //Check to see that the save file exists. If it does, clear the robots and score.
        //Otherwise, log to the console that there is no save game.

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
  
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            PlayerData save = (PlayerData)bf.Deserialize(file);
            file.Close();

            curScore = save.curScore;
            highScore = save.highScore;
            lives = save.lives;
            curLevel = save.curLevel;

            curScoreText.text = "Score: " + save.curScore;
            curLevelText.text = "Level: " + save.curLevel;
            livesText.text = "Lives: " + save.lives;
            highScoreText.text = "Highscore: " + save.highScore;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Save");
        }
        buildLevel();
    }



    public void resumeGame()
    {
        isPaused = false;
    }


    public void quitGame()
    {
        Debug.Log("Closing Application");
        Application.Quit();
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            hitESC = true;
            isPaused = true;
        }
    }

    
    public void destroyAsteroid(GameObject asteroid)
    {
        asteroids.Remove(asteroid);
        Destroy(asteroid);
        //Debug.Log(asteroids.Count);
    }

    public void addAsteroid(GameObject asteroid)
    {
        asteroids.Add(asteroid);
    }
       

    public void destroySaucer(GameObject saucer)
    {
        saucers.Remove(saucer);
        Destroy(saucer);
    }

    public void addSaucer(GameObject saucer)
    {
        asteroids.Add(saucer);
    }

    public void spawnSaucer()
    {
        float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject saucerInstance = Instantiate(saucerPref, spawnPosition, Quaternion.identity);
        saucers.Add(saucerInstance);
    }

}