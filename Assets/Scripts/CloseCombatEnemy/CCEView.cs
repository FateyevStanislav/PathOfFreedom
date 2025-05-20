using System.Collections;
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

    void Die()
    {
        foreach (var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
        GetComponent<CCEControler>().enabled = false;
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
