using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameMaster : MonoBehaviour
{
    public float playerPoints;
    public float playerScore;
    public float[] levelPoints;
    public float playerLives = 3;
    public int currentLevel = 1;
    public BallController bController;
    public Text livesText;
    public Text scoreText;
    public Text endLevelText;

    
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Score: " + playerScore;
        endLevelText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Score: " + playerScore;
        
        
        if (playerLives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (playerPoints >= levelPoints[currentLevel - 1])
        {
            endLevelText.enabled = true;
            playerPoints = 0;
            GameObject gObject = GameObject.FindGameObjectWithTag("Ball");
            bController = gObject.GetComponent<BallController>();
            gObject.transform.position = bController.startPosition;
            bController.ballRigidbody.velocity = Vector3.zero;
            bController.ballLaunched = false;

            if(currentLevel == 1){
                endLevelText.text = "Grammar School Complete";
            }
            else if(currentLevel == 2){
                endLevelText.text = "Middle School Complete";
            }
            else if(currentLevel == 3){
                endLevelText.text = "High School Complete";
            }
            else if(currentLevel == 4){
                endLevelText.text = "College Complete";
            }
            else{
                SceneManager.LoadScene("WinScene");
            }

            Invoke("IncrementLevel", 3);           
        }
        
    }

    void IncrementLevel(){
        if(currentLevel == 1){
            SceneManager.LoadScene("Level2");
        }
        else if(currentLevel == 2){
            SceneManager.LoadScene("Level3");
        }
        else if(currentLevel == 3){
            SceneManager.LoadScene("Level4");
        }
        else if(currentLevel == 4){
           SceneManager.LoadScene("Level5");
        }        
        endLevelText.enabled = false;
        currentLevel++;
        

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
