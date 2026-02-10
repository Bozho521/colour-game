using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float acceleration = 25.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float mayJumpTime = 0.5f;
    [SerializeField] private float jumpBufferTime = 0.2f;


    [Header("Wall Jump Settings")]
    [SerializeField] private float wallJumpHorizontalForce = 8f;
    [SerializeField] private float wallJumpVerticalForce = 10f;

    private Rigidbody2D rb;
    private Collider2D coll;
    private float horizontalInput;
    private float verticalInput;
    private float jumpCooldownTimer;
    private float mayJumpCounter;
    private float jumpBufferCounter;
    private float originalGravity;

    private bool facingRight = true;
    private bool isOnLadder = false;

    private bool isTouchingWall = false;
    private int wallDirection = 0;
    private int lastWallJumpDirection = 0;

    private Transform playerSprite;
    public GameObject Canvas;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        originalGravity = rb.gravityScale;
        playerSprite = transform.GetChild(0);
    }

    public void SetOnLadder(bool state)
    {
        isOnLadder = state;
        rb.gravityScale = state ? 0f : originalGravity;
        if (state) rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    }

    bool isGrounded()
    {
        float extraHeight = 0.1f;
        Vector2 boxSize = new Vector2(coll.bounds.size.x * 0.6f, coll.bounds.size.y);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(coll.bounds.center, boxSize, 0f, Vector2.down, extraHeight, groundLayer);

        foreach (var hit in hits)
        {
            if (hit.collider == coll) continue;

            if (hit.collider.isTrigger) continue;

            return true;
        }

        return false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (jumpCooldownTimer > 0f) jumpCooldownTimer -= Time.deltaTime;

        if (isGrounded() && jumpCooldownTimer <= 0f)
        {
            mayJumpCounter = mayJumpTime;
            lastWallJumpDirection = 0;
        }
        else
        {
            mayJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f)
        {
            if (mayJumpCounter > 0f || isOnLadder)
            {
                PerformJump();
            }
            else if (isTouchingWall && wallDirection != lastWallJumpDirection)
            {
                PerformWallJump(wallDirection);
                lastWallJumpDirection = wallDirection;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void PerformJump()
    {
        SetOnLadder(false);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        float jumpForce = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        mayJumpCounter = 0f;
        jumpBufferCounter = 0f;
        jumpCooldownTimer = 0.2f;
    }

    private void PerformWallJump(int direction)
    {
        SetOnLadder(false);

        rb.linearVelocity = Vector2.zero;

        float verticalForce = Mathf.Sqrt(2f * wallJumpVerticalForce * Mathf.Abs(Physics2D.gravity.y));

        rb.AddForce(
            new Vector2(direction * wallJumpHorizontalForce, verticalForce),
            ForceMode2D.Impulse
        );

        jumpBufferCounter = 0f;
        mayJumpCounter = 0f;
        jumpCooldownTimer = 0.2f;

        if ((direction > 0 && facingRight) || (direction < 0 && !facingRight))
            Flip();
    }

    void FixedUpdate()
    {
        if (isOnLadder)
        {
            rb.linearVelocity = new Vector2(
                horizontalInput * topSpeed * 0.4f,
                verticalInput * climbSpeed
            );
            return;
        }

        float targetSpeed = horizontalInput * topSpeed;
        float accelRate = isGrounded() ? acceleration : acceleration * 0.6f;

        float speedDiff = targetSpeed - rb.linearVelocity.x;
        float movement = speedDiff * accelRate * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x + movement,
            rb.linearVelocity.y
        );

        if (isGrounded() && horizontalInput == 0f)
        {
            rb.linearVelocity = new Vector2(
                Mathf.Lerp(rb.linearVelocity.x, 0f, 0.2f),
                rb.linearVelocity.y
            );
        }

        if (horizontalInput > 0 && facingRight) Flip();
        else if (horizontalInput < 0 && !facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        playerSprite.localScale = new Vector3(-playerSprite.localScale.x, playerSprite.localScale.y, 1);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0)
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            if (Mathf.Abs(normal.x) > 0.5f && Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                isTouchingWall = true;
                wallDirection = normal.x > 0 ? 1 : -1;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0)
            return;

        isTouchingWall = false;
        wallDirection = 0;
    }
}