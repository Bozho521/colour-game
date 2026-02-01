using UnityEngine;
using System.Collections.Generic;

public class HatSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ColorZoneGroup
    {
        public string zoneName;
        public Color floorColor;
        public List<HatCollectible> spawnPoints;
    }

    [Header("Setup")]
    [SerializeField] private ColorZoneGroup[] zoneGroups;
    [SerializeField] private Color[] colors = new Color[4];

    void Start()
    {
        if (colors.Length < 4 || zoneGroups.Length < 3)
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
        if (player != null) player.AddHat(shuffledColors[0]);

        List<ColorZoneGroup> availableZones = new List<ColorZoneGroup>(zoneGroups);
        int hatsSpawned = 0;
        int colorIndex = 1;

        while (hatsSpawned < 3 && colorIndex < 4 && availableZones.Count > 0)
        {
            int zoneIdx = Random.Range(0, availableZones.Count);
            ColorZoneGroup currentZone = availableZones[zoneIdx];
            Color targetColor = shuffledColors[colorIndex];

            if (ColorsMatch(targetColor, currentZone.floorColor))
            {
                if (colorIndex + 1 < 4)
                {
                    shuffledColors[colorIndex] = shuffledColors[colorIndex + 1];
                    shuffledColors[colorIndex + 1] = targetColor;
                    targetColor = shuffledColors[colorIndex];
                }
                else
                {
                    shuffledColors[colorIndex] = shuffledColors[0];
                    shuffledColors[0] = targetColor;
                    targetColor = shuffledColors[colorIndex];
                }
            }

            if (currentZone.spawnPoints.Count > 0)
            {
                int pointIdx = Random.Range(0, currentZone.spawnPoints.Count);
                HatCollectible chosenHat = currentZone.spawnPoints[pointIdx];

                chosenHat.ActivateHat(targetColor);

                currentZone.spawnPoints.RemoveAt(pointIdx);

                hatsSpawned++;
                colorIndex++;
                availableZones.RemoveAt(zoneIdx);
            }
            else
            {
                availableZones.RemoveAt(zoneIdx);
            }
        }

        foreach (var zone in zoneGroups)
        {
            foreach (var leftover in zone.spawnPoints)
            {
                if (leftover != null) Destroy(leftover.gameObject);
            }
        }
    }

    private bool ColorsMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.1f &&
               Mathf.Abs(a.g - b.g) < 0.1f &&
               Mathf.Abs(a.b - b.b) < 0.1f;
    }
}