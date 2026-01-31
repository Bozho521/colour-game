using UnityEngine;

public class ColorLadder : MonoBehaviour
{
    public int ladderColorIndex;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pMove = other.GetComponent<PlayerMovement>();
            if (pMove != null)
            {
                bool isVisible = (BackGround_switch.CurrentColorIndex != ladderColorIndex);

                if (isVisible)
                {
                    pMove.SetOnLadder(true);
                }
                else
                {
                    pMove.SetOnLadder(false);
                }
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
}