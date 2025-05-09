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
    private float musketRotateAngle = 70f;

    private void Awake()
    {
        model = new MusketeerModel();
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
    }

    private void Update()
    {
        UpdateHealtBarValue();
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
    }

    internal void NormalizedSword()
    {
        Musket.Rotate(0, 0, musketRotateAngle);
    }

    void Die()
    {
        foreach (var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
        GetComponent<MusketeerControler>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);
        StartCoroutine(DieAfterFall());
    }

    private IEnumerator DieAfterFall()
    {
        yield return new WaitUntil(() => rb.linearVelocityY < 0);
        if (!IsOnScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0;
    }
}
