using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyBrick : MonoBehaviour
{
    public int numberOfHits = 0;
    public int maxHits;
    public SpriteRenderer brickSprite;
    public float brickValue;
    public bool doesExplode = false;
    public float blastRadius = 5.0f;

    public GameMaster gameMaster;

    void Awake(){
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;        
    }

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        numberOfHits++;
        if(numberOfHits < maxHits){
            brickSprite.color = Color.magenta;
        }
        if(other.gameObject.CompareTag("Ball")){
            BallController bControl = other.gameObject.GetComponent<BallController>();            
            bControl.lastCollision = Time.realtimeSinceStartup;
            //Debug.Log("Last Collision: " + bControl.lastCollision);
        }

        if (numberOfHits >= maxHits)
        {
            gameMaster.playerPoints = gameMaster.playerPoints + brickValue;  
            gameMaster.playerScore = gameMaster.playerScore + brickValue;

            //Debug.Log("player points " + gameMaster.playerPoints);        
            if(doesExplode){
                GameObject[] brickList = GameObject.FindGameObjectsWithTag ("Brick");
        
                foreach(GameObject aBrick in brickList)
                {
                    Debug.Log("Brick name " + aBrick.name);
                    Vector2 brickPos = aBrick.gameObject.transform.position;
                    Vector2 myPos = transform.position;
                    float distance = Vector2.Distance(myPos, brickPos);
                    if(distance < blastRadius){
                        //destroy this block
                        Debug.Log("destroying brick");
                        
                        Destroy(aBrick.gameObject, 0.1f);
                    }                    
                }                
            }else{
                Invoke("DestroyDelayed", 0.05f);
            }
        }
    }

    void DestroyDelayed(){
        Destroy(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //GameObject.FindGameObjectWithTag
        //Debug.Log("Scene Loaded2");
        GameObject gObject = GameObject.FindGameObjectWithTag("GameMaster");
        //Debug.Log("gameObject " + gObject.name);

        gameMaster = gObject.GetComponent<GameMaster>();

    }

}
