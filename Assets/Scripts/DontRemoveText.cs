using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRemoveText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Canvas");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }        
}
