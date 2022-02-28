using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float playerInput;
    public float paddleSpeed;
    private AudioSource playerAudio;
    public AudioClip powerSound;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        playerInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * paddleSpeed * playerInput);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PowerUp")){
            PowerUp power = other.gameObject.GetComponent<PowerUp>();
            playerAudio.PlayOneShot(powerSound, 1.0f);
            Debug.Log("powerup name " + other.gameObject.name);

            GameObject gObject = GameObject.FindGameObjectWithTag("GameMaster");
            GameMaster gameMaster = gObject.GetComponent<GameMaster>();
            gameMaster.activatePowerup(power.powerupType, power.powerupDuration, power.powerupAmount);
            Destroy(other.gameObject);
        }
    }

}
