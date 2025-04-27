public class CCEModel
{
    internal readonly float maxHealth = 40f;
    internal float Health;
    internal float Damage = 15f;
    internal float Cooldown;

    internal void Initialise()
    {
        Health = maxHealth;
    }
}
