using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log ("Quit Button Pushed");
    }

    public void StartGame ()
    {
        SceneManager.LoadScene ("DemoLevel");
    }

    public void HowTo ()
    {
        SceneManager.LoadScene ("HowToPlay");
    }

    public void Back ()
    {
        SceneManager.LoadScene ("StartMenu");
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
