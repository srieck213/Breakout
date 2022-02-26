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
    public float powerupDelay = 1.0f;
    public GameObject[] powerupPrefabs;
    private IEnumerator spawner;
    private bool wonGame = false;

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

        if (!wonGame && playerPoints >= levelPoints[currentLevel - 1])
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
                wonGame = true;
            }
            stopPowerupSpawner();
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

    IEnumerator SpawnPowerup(){
        while(true){
            yield return new WaitForSeconds(powerupDelay);
            powerupDelay = Random.Range(1.0f, 3.0f);
            int powerupIndex = 0;
            if(powerupPrefabs.Length > 1){ 
                powerupIndex = Random.Range(0, powerupPrefabs.Length - 1);
            }
            float randomX = Random.Range(-10.0f, 10.0f);
            string powerupName = powerupPrefabs[powerupIndex].name;            
            GameObject myPowerup = Instantiate(powerupPrefabs[powerupIndex], new Vector2(randomX, 5.5f), powerupPrefabs[powerupIndex].transform.rotation);            
            PowerUp pController = myPowerup.GetComponent<PowerUp>();
            pController.speed = Random.Range(7.0f, 15.0f);
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

    public void startPowerupSpawner(){
        spawner = SpawnPowerup();
        StartCoroutine(spawner);
    }

    public void stopPowerupSpawner(){
        StopCoroutine(spawner);
    }

    public void activatePowerup(string powerupType, float duration, float amount){
        Debug.Log("Activate " + powerupType + " duration " + duration + " for " + amount);
        if(powerupType == "bigpaddle"){
            GameObject paddleObject = GameObject.FindGameObjectWithTag("Player");
            //paddleObject
            paddleObject.transform.localScale = new Vector2(paddleObject.transform.localScale.x * amount, paddleObject.transform.localScale.y);
            object[] deactivateParams = new object[3]{powerupType, duration, amount};
            StartCoroutine(DeactivatePowerup(deactivateParams));
        }


    }

    IEnumerator DeactivatePowerup(object[] dParams){
        string powerupType = (string)dParams[0];
        float duration = (float)dParams[1];        
        float amount = (float)dParams[2];
        yield return new WaitForSeconds(duration);
        //undo the powerup
        if(powerupType == "bigpaddle"){
            GameObject paddleObject = GameObject.FindGameObjectWithTag("Player");
            paddleObject.transform.localScale = new Vector2(paddleObject.transform.localScale.x / amount, paddleObject.transform.localScale.y);
        }


    }




}
