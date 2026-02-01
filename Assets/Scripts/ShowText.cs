using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    public string textValue;
    public TextMeshProUGUI textElement;
    public GameObject Canvas;
    private string sceneName;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = "Something else";
        StartCoroutine(Instruction());
        Debug.Log("timer started");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int buildIndex = currentScene.buildIndex;
        Canvas.SetActive(false);
    }
    IEnumerator Instruction()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            yield return new WaitForSeconds(5);
            Canvas.SetActive(true);
            textElement.text = "Get me on a Button! Space = Jump!";
            Debug.Log("5 seconds has passed");
            yield return new WaitForSeconds(10);
            Canvas.SetActive(false);
            Debug.Log("canvas deactivate");
        }

        else
        {
            yield return new WaitForSeconds(5);
            Canvas.SetActive(true);
            textElement.text = "Now, where's my hat? It's here somewhere";
            Debug.Log("5 seconds has passed");
            yield return new WaitForSeconds(10);
            Canvas.SetActive(false);
            Debug.Log("canvas deactivate");
        }

    }


    public void GotAHat()
    {
      

    }
   
    
    // Update is called once per frame
    void Update()

    {


    }
}
