using UnityEngine;

public class ColorLadder : MonoBehaviour
{
    [Header("Sync Settings")]
    public Color myLadderColor;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = myLadderColor;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pMove = other.GetComponent<PlayerMovement>();
            if (pMove != null)
            {
                bool isVisible = !ColorsMatch(BackGround_switch.Instance.GetCurrentColor(), myLadderColor);

                pMove.SetOnLadder(isVisible);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pMove = other.GetComponent<PlayerMovement>();
            if (pMove != null)
            {
                pMove.SetOnLadder(false);
            }
        }
    }

    bool ColorsMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.1f &&
               Mathf.Abs(a.g - b.g) < 0.1f &&
               Mathf.Abs(a.b - b.b) < 0.1f;
    }
}