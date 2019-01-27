using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    void Start()
    {   
        StartCoroutine("ShowFirstText");
    }

    // Update is called once per frame
    IEnumerator ShowFirstText()
    {
        yield return new WaitForSeconds(2.5f);
        Text1.SetActive(true);
        StartCoroutine("ShowSecondText");
    }

    IEnumerator ShowSecondText()
    {
        yield return new WaitForSeconds(2.5f);
        Text1.SetActive(false);
        Text2.SetActive(true);
        StartCoroutine("HideSecondText");
    }

    IEnumerator HideSecondText()
    {
        yield return new WaitForSeconds(2.5f);
        Text2.SetActive(false);
        StartCoroutine("ShowThirdText");
    }

    IEnumerator ShowThirdText()
    {
        yield return new WaitForSeconds(2.5f);
        Text3.SetActive(true);
        StartCoroutine("HideThirdText");
    }

    IEnumerator HideThirdText()
    {
        yield return new WaitForSeconds(2.5f);
        Text3.SetActive(false);
        StartCoroutine("LoadFirstLevel");

    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("TestScene-Sewer");
    }
}
