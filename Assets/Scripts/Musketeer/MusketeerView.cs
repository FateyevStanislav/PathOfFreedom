using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class MusketeerView : MonoBehaviour
{
    internal PlayerModel player;
    internal Rigidbody2D rb;
    internal MusketeerModel model;
    internal Image HealthBar;
    internal Transform Musket;
    [SerializeField] private Bullet Bullet;
    internal Transform bulletStart;
    private float musketRotateAngle = 70f;

    private void Awake()
    {
        model = GetComponent<MusketeerControler>().model;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        HealthBar = GameObject.Find("MusketeerHealthBar").GetComponent<Image>();
        HealthBar.type = Image.Type.Filled;
        HealthBar.fillMethod = Image.FillMethod.Horizontal;
        HealthBar.fillAmount = 1f;
        Musket = transform.Find("Musket");
        bulletStart = GameObject.Find("BulletStart").transform;
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

    internal void Initialise(MusketeerModel modelRef)
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
        Musket.Rotate(0, 0, -musketRotateAngle);
        ShootBullet();
    }

    private void ShootBullet()
    {
        Bullet newBullet = Instantiate(Bullet, bulletStart.position, Quaternion.identity);
        newBullet.Initialize(model.IsFacingOnPlayer);
    }

    internal void NormalizedSword()
    {
        Musket.Rotate(0, 0, musketRotateAngle);
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
