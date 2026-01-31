using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using UnityEditor.Rendering.Canvas.ShaderGraph;

public class BackGround_switch : MonoBehaviour
{
    public Color[] colors = new Color[4];
    public static int CurrentColorIndex = 0;

    [SerializeField] private SpriteRenderer transition;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float transitionSpeed = 1.0f;

    private float lerpTarget;
    private float currentFade = 0f;
    private Coroutine colorCoroutine;

    [SerializeField] bool readyToSwitch = true;

    void Start()
    {
        if (colors.Length > 0) Debug.Log("Initial Color: " + colors[CurrentColorIndex]);
        //StartCoroutine(SetColor(colors[CurrentColorIndex]));
    }

    void Update()
    {
        if (colors.Length > 0 && Mouse.current != null && readyToSwitch && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CurrentColorIndex = (CurrentColorIndex + 1) % colors.Length;
            if (colorCoroutine != null) StopCoroutine(colorCoroutine);
            StartCoroutine(SetColor(colors[CurrentColorIndex]));
        }

        currentFade = Mathf.MoveTowards(currentFade, lerpTarget, Time.deltaTime * transitionSpeed);
        transition.material.SetFloat("_FadeAmount", currentFade);
    }

    public IEnumerator SetColor(Color color)
    {
        readyToSwitch = false;
        transition.material.SetColor("_Color1", color);
        lerpTarget = 1.0f;
        yield return new WaitForSeconds(1f / transitionSpeed);
        background.color = color;
        lerpTarget = 0.0f;
        readyToSwitch = true;
    }
}