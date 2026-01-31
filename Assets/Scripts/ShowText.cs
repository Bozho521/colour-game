using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    public string textValue;
    public TextMeshProUGUI textElement;
    public GameObject Canvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = "Something else";
        StartCoroutine(Instruction());
        Debug.Log("timer started");
    }
    IEnumerator Instruction()
    {
        yield return new WaitForSeconds(5);
        Canvas.SetActive(true);
        textElement.text = "Get me on a Button! Space = Jump!";
        Debug.Log("5 seconds has passes");
    }

    // Update is called once per frame
    void Update()

    {


    }
}
