using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        Time.timeScale = 0;
        print("asd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        Time.timeScale = 1;
        print("asd2");
        GameObject.Find("StartCanvas").SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

}
