using UnityEngine;
using System.Collections.Generic;

public class PlayerHats : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] hatSlots;

    private List<Color> collectedColors = new List<Color>();
    private int collectedCount = 0;

    void Start()
    {
        foreach (var slot in hatSlots)
        {
            if (slot != null) slot.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToColor(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToColor(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToColor(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchToColor(3);
    }

    public void AddHat(Color color)
    {
        if (collectedCount < hatSlots.Length)
        {
            hatSlots[collectedCount].gameObject.SetActive(true);
            hatSlots[collectedCount].color = color;
            collectedColors.Add(color);

            if (collectedCount == 0)
            {
                BackGround_switch.Instance.SwitchToCollectedColor(color, 0, true);
            }

            collectedCount++;
        }
    }

    private void SwitchToColor(int index)
    {
        if (index < collectedColors.Count)
        {
            Debug.Log($"Switching to Unlocked Color: {index + 1}");

            BackGround_switch.Instance.SwitchToCollectedColor(collectedColors[index], index);
        }
        else
        {
            Debug.Log("You haven't collected that hat yet!");
        }
    }
}