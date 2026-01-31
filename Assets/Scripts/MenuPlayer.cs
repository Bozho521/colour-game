using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] private MainMenu mm;
    [SerializeField] private Collider2D playCollider;
    [SerializeField] private Collider2D quitCollider;

    private bool loading = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playCollider && !loading)
        {
            loading = true;
            StartCoroutine(mm.PlayWithDelay(2f));
        }
        else if (collision == quitCollider && !loading)
        {
            loading = true;
            StartCoroutine(mm.QuitWithDelay(1f));
        }
    }
}
