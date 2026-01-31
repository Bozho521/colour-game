using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator PlayWithDelay(float delay)
    {
        if (BackGround_switch.Instance != null)
        {
            Color targetColor = BackGround_switch.Instance.colors[1];
            BackGround_switch.Instance.SwitchToCollectedColor(targetColor, 1);
        }

        yield return new WaitForSeconds(delay);
        PlayGame();
    }

    public IEnumerator QuitWithDelay(float delay)
    {
        if (BackGround_switch.Instance != null)
        {
            Color targetColor = BackGround_switch.Instance.colors[0];
            BackGround_switch.Instance.SwitchToCollectedColor(targetColor, 0);
        }

        yield return new WaitForSeconds(delay);
        QuitGame();
    }
}