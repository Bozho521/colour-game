using UnityEngine;

public class ColorTile : MonoBehaviour
{
    public int tileColorIndex;
    private Collider2D _collider2D;
    private GameObject _player;
    private Collider2D _playerCol;
    private Rigidbody2D _playerRb;

    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerCol = _player.GetComponent<Collider2D>();
            _playerRb = _player.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_collider2D == null || _player == null) return;

        bool shouldBeTrigger = (BackGround_switch.CurrentColorIndex == tileColorIndex);

        if (_collider2D.isTrigger && !shouldBeTrigger)
        {
            _collider2D.isTrigger = false;
            ForceTeleportToTop();
        }
        else
        {
            _collider2D.isTrigger = shouldBeTrigger;
        }
    }

    void ForceTeleportToTop()
    {
        ColliderDistance2D dist = _collider2D.Distance(_playerCol);

        if (dist.isValid && dist.distance < 0)
        {
            _player.transform.position += (Vector3)(dist.normal * Mathf.Abs(dist.distance));

            if (_playerRb != null)
            {
                float dot = Vector2.Dot(_playerRb.linearVelocity, dist.normal);
                if (dot < 0)
                {
                    _playerRb.linearVelocity -= dist.normal * dot;
                }
            }
        }
    }
}