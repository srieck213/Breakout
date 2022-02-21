using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalEnd;
    private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = portalEnd.GetComponent<BoxCollider2D>();
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
            myCollider.isTrigger = false;
            other.gameObject.transform.position = new Vector2(portalEnd.transform.position.x, portalEnd.transform.position.y);
            Invoke("ActivatePortal", 0.5f);
        }
    }    
}
