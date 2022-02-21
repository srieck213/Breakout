using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameMaster : MonoBehaviour
{
    public float playerPoints;
    public float[] levelPoints;
    public float playerLives = 3;
    public int currentLevel = 1;
    public Text livesText;
    public Text scoreText;

    
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Score: " + playerPoints;
    }

    // Update is called once per frame
    void Update()
    {
        {
            livesText.text = "Lives: " + playerLives;
            scoreText.text = "Score: " + playerPoints;
        }
        
        if (playerLives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (playerPoints >= levelPoints[currentLevel - 1])
        {
            if(currentLevel == 1){
                currentLevel++;
                playerPoints = 0;
                SceneManager.LoadScene("Level2");
            }
            else if(currentLevel == 2){
                currentLevel++;
                playerPoints = 0;
                SceneManager.LoadScene("Level3");
            }
            else if(currentLevel == 3){
                currentLevel++;
                playerPoints = 0;
                SceneManager.LoadScene("Level4");
            }
            else if(currentLevel == 4){
                currentLevel++;
                playerPoints = 0;
                SceneManager.LoadScene("Level5");
            }
            else{
                SceneManager.LoadScene("WinScene");
            }
           
        }
        
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameMaster");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }    
}
