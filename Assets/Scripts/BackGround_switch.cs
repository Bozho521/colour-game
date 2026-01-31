using UnityEngine;
using UnityEngine.InputSystem;

public class BackGround_switch : MonoBehaviour
{
    public Color[] colors = new Color[4];
    public static int CurrentColorIndex = 0;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (colors.Length > 0) UpdateColor();
    }

    void Update()
    {
        if (colors.Length > 0 && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CurrentColorIndex = (CurrentColorIndex + 1) % colors.Length;
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        _renderer.material.color = colors[CurrentColorIndex];
    }
}