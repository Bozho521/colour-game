using UnityEngine;

public class HatCollectible : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color myColor;
    private bool isCollected = false;

    public void ActivateHat(Color colorToSet)
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        myColor = colorToSet;
        sr.color = myColor;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            PlayerHats hatManager = other.GetComponent<PlayerHats>();
            if (hatManager != null)
            {
                isCollected = true;
                hatManager.AddHat(myColor);
                Destroy(gameObject);
            }
        }
    }
}