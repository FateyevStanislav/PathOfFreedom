using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private float maxJumpTime = 0.3f;

    private PhysicsMaterial2D playerMaterial;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isFacingRight = true;
    private float moveInput;
    private LayerMask ground;
    private Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 3;
        groundCheck = transform.Find("GroundCheck");
        ground = LayerMask.GetMask("Platform");
        playerMaterial = new PhysicsMaterial2D()
        {
            name = "PlayerMaterial",
            friction = 0f
        };
        rb.sharedMaterial = playerMaterial;
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocityY);
        }
        else if(isGrounded)
        {
            rb.linearVelocityX = 0;
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;
        }
        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {   
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}