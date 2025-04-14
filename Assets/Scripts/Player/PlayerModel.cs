using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerModel
{
    [Header("Moshion Parametrs")]
    internal readonly float Speed = 7f;
    internal readonly float JumpForce = 11f;
    internal readonly float MaxJumpTime = 0.2f;
    internal readonly float GroundCheckRadius = 0.3f;

    [Header("Runtime State")]
    internal bool IsOnGround;
    internal bool IsJumping;
    internal bool IsFacingRight = true;
    internal float JumpTimeCounter;
    internal float MoveInput;
    internal readonly float maxHealth = 100f;
    internal float Health;

    internal PhysicsMaterial2D PlayerMaterial;
    internal LayerMask GroundMask;

    internal void Initialise()
    {
        PlayerMaterial = new PhysicsMaterial2D()
        {
            name = "PlayerMaterial",
            friction = 0f
        };
        Health = maxHealth; 
    }

    internal void UpdateJumpTimer()
    {
        if (JumpTimeCounter > 0)
        {
            JumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            IsJumping = false;
        }
    }

    internal void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}