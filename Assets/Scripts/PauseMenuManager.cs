using System;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject finalHat;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // Resume the game
        transform.GetChild(1).gameObject.SetActive(false); // Hide pause menu UI
    }

    private int pauseID = 1; // Assuming the pause menu is the second child
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu(pauseID);
        }
    }

    public void ToggleMenu(int menuID)
    {
        if (Time.timeScale == 1f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f; // Pause the game
            transform.GetChild(menuID).gameObject.SetActive(true); // Show pause menu UI
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f; // Resume the game
            transform.GetChild(menuID).gameObject.SetActive(false); // Hide pause menu UI
        }
    }
}
