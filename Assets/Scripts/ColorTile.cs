using UnityEngine;

public class ColorTile : MonoBehaviour
{
    [SerializeField] private float switchBuffer = 0.1f;

    public Color myTileColor;

    private float _bufferTimer;
    private bool _targetTriggerState;

    private Collider2D _collider2D;
    private GameObject _player;
    private Collider2D _playerCol;
    private Rigidbody2D _playerRb;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer != null) _spriteRenderer.color = myTileColor;

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

        bool shouldBeTrigger = ColorsMatch(BackGround_switch.Instance.GetCurrentColor(), myTileColor);

        if (shouldBeTrigger != _targetTriggerState)
        {
            _targetTriggerState = shouldBeTrigger;
            _bufferTimer = switchBuffer;
        }

        if (_bufferTimer > 0)
        {
            _bufferTimer -= Time.deltaTime;
        }
        else
        {
            ApplyColliderLogic(_targetTriggerState);
        }
    }

    bool ColorsMatch(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < 0.1f &&
               Mathf.Abs(a.g - b.g) < 0.1f &&
               Mathf.Abs(a.b - b.b) < 0.1f;
    }

    void ApplyColliderLogic(bool shouldBeTrigger)
    {
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
        if (_playerCol == null) return;
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