public class CCEModel
{
    internal readonly float maxHealth = 40f;
    internal float Health;
    internal float Damage = 15f;
    internal float AttackDuration = 0.5f;
    internal float Cooldown = 2f;
    internal float CooldownCounter;
    internal float AttackRange = 8f;
    public bool IsHitting;
    internal bool IsFacingOnPlayer;
    internal bool isAlive = true;

    internal void Initialise()
    {
        Health = maxHealth;
    }
}
