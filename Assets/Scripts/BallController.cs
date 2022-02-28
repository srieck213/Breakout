using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public bool ballLaunched = false;
    public Rigidbody2D ballRigidbody;
    public Vector2[] startDirections;
    public int randomNumber;
    public float ballForce;
    public Vector3 startPosition;
    public GameMaster gameMaster; 
    public float minimumSpeed = 5.5f;
    public float maximumSpeed = 5.7f;
    public float lastCollision;
    public GameObject thePaddle;
    private AudioSource playerAudio;
    public AudioClip defeatSound;
    public AudioClip bounceSound;
    public AudioClip breakSound;
    public AudioClip explodeSound;
    
    

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        lastCollision = Time.realtimeSinceStartup;     
        playerAudio = GetComponent<AudioSource>();
         
    }

    void Awake(){
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ballLaunched)
        {
            //launch ball
            randomNumber = Random.Range(0, startDirections.Length);
            ballRigidbody.AddForce(startDirections[randomNumber] * ballForce, ForceMode2D.Impulse);
            ballLaunched = true;
            gameMaster.startPowerupSpawner();  
        }

        if(!ballLaunched){
            transform.position = new Vector2(thePaddle.transform.position.x, transform.position.y);            
        }
        if  (Input.GetKeyDown(KeyCode.R))
        {
            ballRigidbody.velocity = Vector3.zero;
            transform.position = startPosition;
            ballLaunched = false;
            gameMaster.stopPowerupSpawner(); 
        }

    }

    void LateUpdate(){
        float diff = Time.realtimeSinceStartup - this.lastCollision;
        if(diff > 0.1){
            //only adjust speed if not recent collision
            if(ballRigidbody.velocity.magnitude < minimumSpeed){
                //Debug.Log("Velocity is " + ballRigidbody.velocity.magnitude + " increasing");
                ballRigidbody.velocity = ballRigidbody.velocity.normalized * (maximumSpeed - 0.1f);
            }else if(ballRigidbody.velocity.magnitude > maximumSpeed){
                //Debug.Log("Velocity is " + ballRigidbody.velocity.magnitude + " decreasing");
                ballRigidbody.velocity = ballRigidbody.velocity.normalized * (minimumSpeed + 0.1f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {   if(other.gameObject.CompareTag("Brick"))
        {
        DestroyBrick dBrick = other.gameObject.GetComponent<DestroyBrick>();
            if(dBrick.doesExplode)
            {
            playerAudio.PlayOneShot(explodeSound, 1.0f);  
            }
            else if(dBrick.maxHits <= 0)
            { 
            playerAudio.PlayOneShot(breakSound, 1.0f);
            }
            else if(dBrick.numberOfHits > 0)
            {
            playerAudio.PlayOneShot(breakSound, 1.0f);
            }
            else 
            {
            playerAudio.PlayOneShot(bounceSound, 1.0f);
            }
        }
        else if(other.gameObject.CompareTag("Player")) 
        {
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DefeatZone")
        {
            ballRigidbody.velocity = Vector3.zero;
            gameMaster.playerLives = gameMaster.playerLives-1;
            transform.position = startPosition;
            ballLaunched = false;
            gameMaster.stopPowerupSpawner(); 
            playerAudio.PlayOneShot(defeatSound, 1.0f);             
        }        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
        GameObject gObject = GameObject.FindGameObjectsWithTag("GameMaster")[0];
        gameMaster = gObject.GetComponent<GameMaster>();
    }

}
