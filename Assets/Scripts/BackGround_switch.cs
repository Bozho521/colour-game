using UnityEngine;
using UnityEngine.InputSystem;

public class BackGround_switch : MonoBehaviour
{
    public Color[] colors = new Color[4];
    private Renderer _renderer;
    private int _currentIndex = 0;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (colors.Length == 0) return;
        _currentIndex = (_currentIndex + 1) % colors.Length;
        _renderer.material.color = colors[_currentIndex];
    }
}