using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerView : MonoBehaviour
{
    private PlayerModel model;
    private Rigidbody2D rb;
    private Transform groundCheck;

    private void Awake()
    {
        model = GetComponent<PlayerController>().model;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 3;
        groundCheck = transform.Find("GroundCheck");
        rb.sharedMaterial = model.PlayerMaterial;
        model.GroundMask = LayerMask.GetMask("Platform");
    }

    internal void Initialise(PlayerModel modelRef)
    {
        model = modelRef;
    }

    internal void Move(float moveInput)
    {
        if (moveInput != 0)
        {
            rb.linearVelocity = new Vector2(moveInput * model.Speed, rb.linearVelocityY);
        }
        else if (model.IsGrounded)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
    }

    internal void Jump(float force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, force);
    }

    internal void Flip()
    {
        model.IsFacingRight = !model.IsFacingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    internal void CheckGround()
    {
        model.IsGrounded = Physics2D.OverlapCircle(groundCheck.position, model.GroundCheckRadius, model.GroundMask);
    }
}
