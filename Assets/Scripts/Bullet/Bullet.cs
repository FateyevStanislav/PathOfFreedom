using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 8f;
    private Rigidbody2D rb;
    private bool isFacingRight;
    internal float Damage = 15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Initialize(bool facingRight)
    {
        isFacingRight = facingRight;
        float direction = isFacingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = GameObject.Find("Player").GetComponent<PlayerView>();
        if (other.CompareTag("Player"))
        {
            player.model.TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}