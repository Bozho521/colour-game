using UnityEngine;
using System.Collections.Generic;

public class HatSpawner : MonoBehaviour
{
    [SerializeField] private List<HatCollectible> allHatsInMap;
    [SerializeField] private Color[] colors = new Color[4];

    void Start()
    {
        if (colors.Length < 4)
        {
            return;
        }

        AssignHats();
    }

    void AssignHats()
    {
        List<HatCollectible> availableHats = new List<HatCollectible>(allHatsInMap);

        int countToSpawn = Mathf.Min(colors.Length, availableHats.Count);

        for (int i = 0; i < countToSpawn; i++)
        {
            if (availableHats.Count == 0) break;

            int randomIndex = Random.Range(0, availableHats.Count);
            HatCollectible chosenHat = availableHats[randomIndex];

            chosenHat.ActivateHat(colors[i]);

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