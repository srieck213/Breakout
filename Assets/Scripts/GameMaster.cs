using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public float playerPoints;
    public float maxLevelPoints;
    public float playerLives = 3;
    public int currentLevel = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (playerPoints >= maxLevelPoints)
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
            }else{
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
