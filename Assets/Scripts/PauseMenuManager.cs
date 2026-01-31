using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1f)
            {
                Time.timeScale = 0f; // Pause the game
                transform.GetChild(0).gameObject.SetActive(true); // Show pause menu UI
            }
            else
            {
                Time.timeScale = 1f; // Resume the game
                transform.GetChild(0).gameObject.SetActive(false); // Hide pause menu UI
            }
        }
    }
}
