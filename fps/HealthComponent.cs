using Godot;

[GlobalClass]
public partial class HealthComponent : Node
{
    private float _health;
    public float Health
    {
        get => _health;
        set {
            _health = value;
            if(_health <= 0)
            {
                Die();
            }
        }
    }

    [Export]
    public float DefaultHealth;

    public override void _Ready()
    {
        Health = DefaultHealth;
    }

    public void Die()
    {
        Player.INSTANCE = null;
        this.GetParent().QueueFree();
        Networking.Instance.TrySendPacket("OtherPlayerDied");
    }
}