using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float acceleration = 25.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] [Tooltip("Coyote Time")] private float mayJump = 0.5f;

    private Rigidbody2D rb;
    private Collider2D coll;
    private float horizontalInput;
    private float verticalInput;
    private float currentSpeed;
    private float jumpCooldownTimer;
    private float originalGravity;

    private bool facingRight = true;
    private bool isOnLadder = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        originalGravity = rb.gravityScale;
    }

    public void SetOnLadder(bool state)
    {
        isOnLadder = state;
        if (isOnLadder)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        else
        {
            rb.gravityScale = originalGravity;
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (jumpCooldownTimer > 0)
            jumpCooldownTimer -= Time.deltaTime;

        // baby's first coyote time
        if (isGrounded() && jumpCooldownTimer <= 0)
            mayJump = 0.5f;
        else
            mayJump -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && (mayJump > 0f || isOnLadder))
        {
            SetOnLadder(false);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(new Vector2(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y))), ForceMode2D.Impulse);

            mayJump = 0f;
            jumpCooldownTimer = 0.2f;
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rb.linearVelocity.x) < topSpeed)
            rb.AddForce(new Vector2(horizontalInput * acceleration, 0));

        float targetX = Mathf.Clamp(rb.linearVelocity.x, -topSpeed, topSpeed);

        float targetY = rb.linearVelocity.y;
        if (isOnLadder)
        {
            targetY = verticalInput * climbSpeed;
        }

        rb.linearVelocity = new Vector2(targetX, targetY);

        if (horizontalInput > 0 && !facingRight) Flip();
        else if (horizontalInput < 0 && facingRight) Flip();
    }
}