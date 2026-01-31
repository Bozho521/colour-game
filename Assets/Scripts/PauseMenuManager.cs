using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // Resume the game
        transform.GetChild(0).gameObject.SetActive(false); // Hide pause menu UI
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1f)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f; // Pause the game
                transform.GetChild(0).gameObject.SetActive(true); // Show pause menu UI
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f; // Resume the game
                transform.GetChild(0).gameObject.SetActive(false); // Hide pause menu UI
            }
        }
    }

}
