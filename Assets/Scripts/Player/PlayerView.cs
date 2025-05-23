using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerView : MonoBehaviour
{
    internal PlayerModel model;
    private Rigidbody2D rb;
    internal Image HealthBar;
    internal Transform Sword;
    private Transform groundCheck;
    private AudioSource hitSound;
    private float swortRotateAngle = 70f;

    private void Awake()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        model = GetComponent<PlayerController>().model;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 3;
        groundCheck = transform.Find("GroundCheck");
        rb.sharedMaterial = model.PlayerMaterial;
        rb.position = GameObject.FindWithTag("Respawn").transform.position;
        model.GroundMask = LayerMask.GetMask("Platform");
        if (sceneIndex != 4 && sceneIndex != 5)
        {
            Sword = transform.Find("PlayerSword");
            Sword.GetComponent<BoxCollider2D>().isTrigger = true;
            HealthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
            HealthBar.type = Image.Type.Filled;
            HealthBar.fillMethod = Image.FillMethod.Horizontal;
            HealthBar.fillAmount = 1f;
            GameObject.Find("Canvas").GetComponent<Canvas>().sortingOrder = 1;
            hitSound = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (HealthBar != null)
        {
            UpdateHealtBarValue();
        }
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

    internal void Hit()
    {
        Sword.Rotate(0, 0, -swortRotateAngle);
        hitSound.Play();
    }

    internal void RaiseSword()
    {
        Sword.Rotate(0, 0, swortRotateAngle);
    }

    internal void CheckGround()
    {
        model.IsOnGround = Physics2D.OverlapCircle(groundCheck.position, model.GroundCheckRadius, model.GroundMask);
    }
}
