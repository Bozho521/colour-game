using UnityEngine;

public class PlayerHats : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] hatSlots; 
    private int collectedCount = 0;

    void Start()
    {
        foreach (var slot in hatSlots) 
        {
            if (slot != null) slot.gameObject.SetActive(false);
        }
    }

    public void AddHat(Color color)
    {
        if (collectedCount < hatSlots.Length)
        {
            hatSlots[collectedCount].gameObject.SetActive(true);
            hatSlots[collectedCount].color = color;
            
            collectedCount++;
        }
    }
}