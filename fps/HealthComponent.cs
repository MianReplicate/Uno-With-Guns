using Godot;

[GlobalClass]
public partial class HealthComponent : Node
{
    [Export] public float health = 100f;
    private float _health
    {
        get => health;
        set {
            health = value;
            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        this.GetParent().QueueFree();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void Heal(float amount)
    {
        health += amount;
    }
}