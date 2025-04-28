using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class CCEView : MonoBehaviour
{
    internal PlayerModel player;
    private Rigidbody2D rb;
    private CCEModel model;
    internal Image HealthBar;
    private void Awake()
    {
        model = new CCEModel();
        model = GetComponent<CCEControler>().model;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        HealthBar = GameObject.Find("CCEHealthBar").GetComponent<Image>();
        HealthBar.type = Image.Type.Filled;
        HealthBar.fillMethod = Image.FillMethod.Horizontal;
        HealthBar.fillAmount = 1f;
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
