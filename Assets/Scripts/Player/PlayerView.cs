using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerView : MonoBehaviour
{
    private PlayerModel model;
    private Rigidbody2D rb;
    internal Image HealthBar;
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
        HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        HealthBar.type = Image.Type.Filled;
        HealthBar.fillMethod = Image.FillMethod.Horizontal;
        HealthBar.fillAmount = 1f;
        GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 1;
    }

    private void Update()
    {
        UpdateHealtBarValue();
    }

    private void UpdateHealtBarValue()
    {
        HealthBar.fillAmount = model.Health / model.maxHealth;
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
        else if (model.IsOnGround)
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
        model.IsOnGround = Physics2D.OverlapCircle(groundCheck.position, model.GroundCheckRadius, model.GroundMask);
    }
}
