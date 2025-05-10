using UnityEngine;

public class CCEControler : MonoBehaviour
{
    internal CCEModel model;
    private CCEView view;
    private PlayerView player;
    private Collider2D bodyCollider;

    private void Awake()
    {
        model = new CCEModel();
        model.Initialise();
        view = GetComponent<CCEView>();
        view.Initialise(model);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerView>();
        bodyCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleHit();
        HadleFlipToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword") 
            && other.IsTouching(bodyCollider)
            && player.model.IsHitting)
        {
            view.TakeDamage(player.model.Damage);
        }
    }

    private void HandleHit()
    {
        if (model.CooldownCounter > 0)
        {
            model.CooldownCounter -= Time.deltaTime;
            return;
        }
        else if (model.IsHitting)
        {
            model.IsHitting = false;
            view.RaiseSword();
            model.CooldownCounter = model.Cooldown;
            return;
        }
        if (CanAttackPlayer() && model.CooldownCounter <= 0 && !model.IsHitting)
        {
            model.IsHitting = true;
            model.CooldownCounter = model.AttackDuration;
            view.Hit();
        }
    }

    private bool CanAttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(
            transform.position, 
            player.transform.position);
        return distanceToPlayer <= model.AttackRange;
    }

    private void HadleFlipToPlayer()
    {
        var playerIsToRight = (player.transform.position.x - transform.position.x) > 0;
        if (playerIsToRight && !model.IsFacingOnPlayer)
        {
            view.Flip();
        }
        else if (!playerIsToRight && model.IsFacingOnPlayer)
        {
            view.Flip();
        }
    }
}
