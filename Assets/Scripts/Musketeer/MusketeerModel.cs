using UnityEngine;

public class MusketeerModel
{
    internal readonly float maxHealth = 40f;
    internal float Health;
    internal float AttackDuration = 0.5f;
    internal float Cooldown = 2f;
    internal float CooldownCounter;
    internal float AttackRange = 15f;
    public bool IsHitting;
    internal bool IsFacingOnPlayer;
    internal GameObject Bullet;
    internal bool isAlive = true;

    internal void Initialise()
    {
        Health = maxHealth;
    }
}
