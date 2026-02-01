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

        List<Color> hatColors = new List<Color> { shuffledColors[1], shuffledColors[2], shuffledColors[3] };

        List<ColorZoneGroup> availableZones = new List<ColorZoneGroup>(zoneGroups);

        foreach (Color targetColor in hatColors)
        {
            bool spawned = false;

            for (int i = 0; i < availableZones.Count; i++)
            {
                ColorZoneGroup zone = availableZones[i];

                if (!ColorsMatch(targetColor, zone.floorColor))
                {
                    if (zone.spawnPoints.Count > 0)
                    {
                        int pointIdx = Random.Range(0, zone.spawnPoints.Count);
                        HatCollectible chosenHat = zone.spawnPoints[pointIdx];

                        chosenHat.ActivateHat(targetColor);

                        zone.spawnPoints.RemoveAt(pointIdx);

                        availableZones.RemoveAt(i);

                        spawned = true;
                        break;
                    }
                }
            }

            if (!spawned && availableZones.Count > 0)
            {
                int fallbackIdx = 0;
                ColorZoneGroup fallbackZone = availableZones[fallbackIdx];
                int pointIdx = Random.Range(0, fallbackZone.spawnPoints.Count);

                fallbackZone.spawnPoints[pointIdx].ActivateHat(targetColor);
                fallbackZone.spawnPoints.RemoveAt(pointIdx);
                availableZones.RemoveAt(fallbackIdx);
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