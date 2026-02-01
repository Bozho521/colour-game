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
        transform.GetChild(0).gameObject.SetActive(false); // Hide pause menu UI
    }

    private int pauseID = 0; // Assuming the pause menu is the first child
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && finalHat != null)
        {
            ToggleMenu(pauseID);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && finalHat == null)
        {
            Debug.Log("Final hat is missing, cannot pause the game.");
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
