using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalEnd;
    private BoxCollider2D myCollider;
    public AudioClip portalSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = portalEnd.GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ActivatePortal(){
        myCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            playerAudio.PlayOneShot(portalSound, 1.0f);
            myCollider.isTrigger = false;
            other.gameObject.transform.position = new Vector2(portalEnd.transform.position.x, portalEnd.transform.position.y);
            Invoke("ActivatePortal", 0.5f);
        }
    }    
}
