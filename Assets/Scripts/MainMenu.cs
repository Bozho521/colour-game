using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private BackGround_switch bgSwitch;

    private void Start()
    {
        bgSwitch = GetComponent<BackGround_switch>();
    }

    public void PlayGame()
    {
        //loads direct to one level, index can be used if multiple levels needed
        SceneManager.LoadScene(1);
        Debug.Log("You would load a level");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
        //quit won't work in the editor
    }

    public IEnumerator PlayWithDelay(float delay)
    {
        StartCoroutine(bgSwitch.SetColor(bgSwitch.colors[1]));
        yield return new WaitForSeconds(delay);
        PlayGame();
    }

    public IEnumerator QuitWithDelay(float delay)
    {
        StartCoroutine(bgSwitch.SetColor(bgSwitch.colors[2]));
        yield return new WaitForSeconds(delay);
        QuitGame();
    }
}
