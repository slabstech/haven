using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject canvas;

    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartGame();
        }
        
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        GameObject.Find("StartCanvas").SetActive(false);
        canvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
