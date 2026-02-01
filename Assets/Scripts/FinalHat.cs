using UnityEngine;

public class FinalHat : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            credits.SetActive(true);
            player.SetActive(false);
            Debug.Log("Player collided with Final Hat, opening credits menu.");
            Destroy(gameObject);
        }
    }
}
