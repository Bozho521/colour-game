using UnityEngine;
using System.Collections.Generic;

public class PlayerHats : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] hatSlots;

    private List<Color> collectedColors = new List<Color>();
    private int collectedCount = 0;

    void Start()
    {
        for (int i = 0; i < hatSlots.Length; i++)
        {
            if (hatSlots[i] != null) hatSlots[i].gameObject.SetActive(false);
        }

        if (BackGround_switch.Instance != null)
        {
            BackGround_switch.Instance.SwitchToCollectedColor(Color.white, -1, true);
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
                SwitchToColor(0);
            }

            collectedCount++;
        }
    }

    private void SwitchToColor(int index)
    {
        if (!BackGround_switch.Instance.IsReady()) return;

        if (index >= 0 && index < collectedColors.Count)
        {
            BackGround_switch.Instance.SwitchToCollectedColor(collectedColors[index], index);
        }
        else
        {
        }
    }
}