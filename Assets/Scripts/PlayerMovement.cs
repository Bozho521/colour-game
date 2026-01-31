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
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    void Update()
    {
        //baby's first coyote time
        if(isGrounded())
            mayJump = 0.5f;
        else
            mayJump -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && mayJump > 0f)
        {
            rb.AddForce(new Vector2(0, Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y))), ForceMode2D.Impulse);
            mayJump = 0f;
        }
    }
}
