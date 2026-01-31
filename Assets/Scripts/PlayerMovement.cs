using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float acceleration = 25.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] [Tooltip("Coyote Time")] private float mayJump = 0.5f;

    private Rigidbody2D rb;
    private Collider2D coll;
    private float horizontalInput;
    private float currentSpeed;
    private float jumpCooldownTimer;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(rb.linearVelocity.x) < topSpeed)
            rb.AddForce(new Vector2(horizontalInput * acceleration, 0));

        currentSpeed = rb.linearVelocity.x;
        currentSpeed = Mathf.Clamp(currentSpeed, -topSpeed, topSpeed);
        rb.linearVelocityX = currentSpeed;

        if (horizontalInput > 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && !facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null && !hit.collider.isTrigger;
    }

    void Update()
    {
        if (jumpCooldownTimer > 0)
            jumpCooldownTimer -= Time.deltaTime;

        // baby's first coyote time
        if (isGrounded() && jumpCooldownTimer <= 0)
            mayJump = 0.5f;
        else
            mayJump -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && mayJump > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(new Vector2(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y))), ForceMode2D.Impulse);

            mayJump = 0f;
            jumpCooldownTimer = 0.2f;
        }
    }
}