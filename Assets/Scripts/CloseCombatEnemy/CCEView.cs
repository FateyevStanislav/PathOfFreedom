using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class CCEView : MonoBehaviour
{
    internal PlayerModel player;
    internal Rigidbody2D rb;
    internal CCEModel model;
    internal Image HealthBar;
    internal Transform Sword;
    private float swortRotateAngle = 70f;

    private void Awake()
    {
        model = new CCEModel();
        model = GetComponent<CCEControler>().model;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        HealthBar = GameObject.Find("CCEHealthBar").GetComponent<Image>();
        HealthBar.type = Image.Type.Filled;
        HealthBar.fillMethod = Image.FillMethod.Horizontal;
        HealthBar.fillAmount = 1f;
        Sword = transform.Find("CCESword");
        Sword.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        UpdateHealtBarValue();
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealtBarValue()
    {
        HealthBar.fillAmount = model.Health / model.maxHealth;
    }

    internal void Initialise(CCEModel modelRef)
    {
        model = modelRef;
    }

    public void TakeDamage(float dmg)
    {
        model.Health -= dmg;
        if (model.Health <= 0)
        {
            Die();
        }
    }

    internal void Jump(float force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, force);
    }

    internal void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        model.IsFacingOnPlayer = !model.IsFacingOnPlayer;
    }

    internal void Hit()
    {
        Sword.Rotate(0, 0, -swortRotateAngle);
    }

    internal void RaiseSword()
    {
        Sword.Rotate(0, 0, swortRotateAngle);
    }

    private void Die()
    {
        model.isAlive = false;
        GetComponent<Collider2D>().enabled = false;
        var controller = GetComponent<CCEControler>();
        if (controller != null) controller.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
    }
}
