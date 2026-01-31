using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 public void PlayGame()
    {
        //loads direct to one level, index can be used if multiple levels needed
        SceneManager.LoadScene("Level");
        Debug.Log("You would load a level");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
        //quit won't work in the editor
    }
}
