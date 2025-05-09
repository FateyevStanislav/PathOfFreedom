using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    internal PlayerModel model;
    private PlayerView view;
    private CCEView CCEnemy;
    private Collider2D bodyCollider;

    private void Awake()
    {
        model = new PlayerModel();
        model.Initialise();
        view = GetComponent<PlayerView>();
        view.Initialise(model);
        CCEnemy = GameObject.Find("CloseCombatEnemy").GetComponent<CCEView>();
        bodyCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleEscape();
        view.CheckGround();
        HandleMoveInput();
        HandleJumpInput();
        HandleFLip();
        HandleHit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            model.TakeDamage(20);
            view.Jump(model.JumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CCESword")
            && other.IsTouching(bodyCollider)
            && CCEnemy.model.IsHitting)
        {
            model.TakeDamage(CCEnemy.model.Damage);
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            Debug.Log("Óðà");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void HandleMoveInput()
    {
        model.MoveInput = Input.GetAxis("Horizontal");
        view.Move(model.MoveInput);
    }

    private void HandleJumpInput()
    {
        if (model.IsOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            model.IsJumping = true;
            model.JumpTimeCounter = model.MaxJumpTime;
            view.Jump(model.JumpForce);
        }
        if (Input.GetKey(KeyCode.Space) && model.IsJumping)
        {
            if (model.JumpTimeCounter > 0)
            {
                view.Jump(model.JumpForce);
                model.UpdateJumpTimer();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            model.IsJumping = false;
        }
    }

    private void HandleFLip()
    {
        if ((model.MoveInput > 0 && !model.IsFacingRight)
            || (model.MoveInput < 0 && model.IsFacingRight))
        {
            view.Flip();
        }
    }

    private void HandleHit()
    {
        if (model.CooldownCounter > 0)
        {
            model.CooldownCounter -= Time.deltaTime;
        }
        else if (model.IsHitting)
        {
            model.IsHitting = false;
            view.RaiseSword();
        }

        if (Input.GetMouseButtonDown(0) && model.CooldownCounter <= 0 && !model.IsHitting)
        {
            model.IsHitting = true;
            model.CooldownCounter = model.Cooldown;
            view.Hit();
        }
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
