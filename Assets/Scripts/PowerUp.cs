using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   public float speed = 7.0f;
   public float powerupDuration = 5.0f;
   public float powerupAmount = 2.0f;
   public string powerupType = "";

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name.IndexOf("PaddleBig") >= 0){
            powerupType = "bigpaddle";
        }
        else if(this.gameObject.name.IndexOf("PaddleSmall") >= 0){
            powerupType = "smallpaddle";
        }
        else if(this.gameObject.name.IndexOf("IncreaseSpeed") >= 0){
            powerupType = "increasespeed";
        }
        else if(this.gameObject.name.IndexOf("DecreaseSpeed") >= 0){
            powerupType = "decreasespeed";
        }
        else if(this.gameObject.name.IndexOf("ExtraLife") >= 0){
            powerupType = "extralife";
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
