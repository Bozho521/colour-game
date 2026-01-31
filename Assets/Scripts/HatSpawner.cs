using UnityEngine;
using System.Collections.Generic;

public class HatSpawner : MonoBehaviour
{
    [SerializeField] private List<HatCollectible> allHatsInMap;
    [SerializeField] private Color[] colors = new Color[4];

    void Start()
    {
        if (colors.Length < 4 || allHatsInMap.Count < 3)
        {
            Debug.LogError("Not enough colors or map triangles assigned!");
            return;
        }

        AssignHats();
    }

    void AssignHats()
    {
        List<Color> shuffledColors = new List<Color>(colors);
        for (int i = 0; i < shuffledColors.Count; i++)
        {
            Color temp = shuffledColors[i];
            int randomIndex = Random.Range(i, shuffledColors.Count);
            shuffledColors[i] = shuffledColors[randomIndex];
            shuffledColors[randomIndex] = temp;
        }

        PlayerHats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHats>();
        if (player != null)
        {
            player.AddHat(shuffledColors[0]);
        }

        List<HatCollectible> availableHats = new List<HatCollectible>(allHatsInMap);

        for (int i = 1; i < 4; i++)
        {
            if (availableHats.Count == 0) break;

            int randomIndex = Random.Range(0, availableHats.Count);
            HatCollectible chosenHat = availableHats[randomIndex];

            chosenHat.ActivateHat(shuffledColors[i]);

            availableHats.RemoveAt(randomIndex);
        }

        foreach (HatCollectible leftover in availableHats)
        {
            if (leftover != null)
            {
                Destroy(leftover.gameObject);
            }
        }
    }
}