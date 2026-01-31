using UnityEngine;
using System.Collections;

public class BackGround_switch : MonoBehaviour
{
    public static BackGround_switch Instance;

    public static int CurrentColorIndex = 0;
    public Color[] colors = new Color[4];

    [SerializeField] private SpriteRenderer transition;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float transitionSpeed = 1.0f;

    private float lerpTarget;
    private float currentFade = 0f;
    private Coroutine colorCoroutine;

    [SerializeField] private bool readyToSwitch = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        transition.material.SetFloat("_FadeAmount", 0);
    }

    void Update()
    {
        currentFade = Mathf.MoveTowards(currentFade, lerpTarget, Time.deltaTime * transitionSpeed);
        transition.material.SetFloat("_FadeAmount", currentFade);
    }

    public void SwitchToCollectedColor(Color targetColor, int index, bool instant = false)
    {
        CurrentColorIndex = index;

        if (instant)
        {
            background.color = targetColor;
            currentFade = 0f;
            lerpTarget = 0f;
            transition.material.SetFloat("_FadeAmount", 0);
            readyToSwitch = true;
            return;
        }

        if (!readyToSwitch) return;

        if (colorCoroutine != null) StopCoroutine(colorCoroutine);
        colorCoroutine = StartCoroutine(TransitionRoutine(targetColor));
    }

    public Color GetCurrentColor()
    {
        return background.color;
    }

    private IEnumerator TransitionRoutine(Color color)
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