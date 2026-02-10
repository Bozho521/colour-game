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



    private Rigidbody2D rb;
    private Collider2D coll;
    private float horizontalInput;
    private float verticalInput;
    private float jumpCooldownTimer;
    private float mayJumpCounter;
    private float jumpBufferCounter;
    private float originalGravity;
    private Transform playerSprite;
    public GameObject Canvas;

    private bool facingRight = true;
    private bool isOnLadder = false;

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
        rb.gravityScale = state ? 0 : originalGravity;
        if (state) rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
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
            mayJumpCounter = mayJumpTime;
        else
            mayJumpCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && (mayJumpCounter > 0f || isOnLadder))
        {
            PerformJump();
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

    void FixedUpdate()
    {
        if (isOnLadder)
        {
            rb.linearVelocity = new Vector2(
                horizontalInput * topSpeed * 0.4f,
                verticalInput * climbSpeed
            );
        }
        else
        {
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
        }

        if (horizontalInput > 0 && facingRight) Flip();
        else if (horizontalInput < 0 && !facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        playerSprite.localScale = new Vector3(-playerSprite.localScale.x, playerSprite.localScale.y, 1);
    }
}