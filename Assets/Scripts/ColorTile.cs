using UnityEngine;

public class ColorTile : MonoBehaviour
{
    public int tileColorIndex;
    private Collider2D _collider2D;

    void Start() => _collider2D = GetComponent<Collider2D>();

    void Update()
    {
        if (_collider2D == null) return;

        bool shouldBeTrigger = (BackGround_switch.CurrentColorIndex == tileColorIndex);

        if (_collider2D.isTrigger && !shouldBeTrigger)
        {
            ForceTeleportToTop();
        }

        _collider2D.isTrigger = shouldBeTrigger;
    }

    void ForceTeleportToTop()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Collider2D playerCol = player.GetComponent<Collider2D>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (_collider2D.IsTouching(playerCol))
        {
            float floorTop = _collider2D.bounds.max.y;
            float playerHalfHeight = playerCol.bounds.extents.y;
            float targetY = floorTop + playerHalfHeight + 0.1f;

            if (player.transform.position.y < targetY)
            {
                if (rb != null) rb.simulated = false;

                player.transform.position = new Vector3(player.transform.position.x, targetY, player.transform.position.z);

                if (rb != null)
                {
                    rb.simulated = true;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                }
            }
        }
    }
}