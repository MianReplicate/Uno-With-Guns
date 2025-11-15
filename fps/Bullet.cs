using Godot;
using System;

public partial class Bullet : Area3D
{
	public Vector3 InitialPosition;
	public Vector3 InitialRotation;
	[Export] public float Speed = 50f;
	public CharacterBody3D Shooter;

	private float _timePassed = 0f;

    public override void _Ready()
    {
        this.GlobalPosition = InitialPosition;
		this.GlobalRotation = InitialRotation;
    }

	public override void _Process(double delta)
    {
        GlobalPosition += -Transform.Basis.Z.Normalized() * Speed * (float) delta;

		_timePassed += (float) delta;
		if(_timePassed > 3)
			this.QueueFree();
	}

	public void OnBodyEntered(Node3D area)
    {
		if(area != Shooter)
        {
			this.QueueFree();
			if(area is CharacterBody3D)
			{
				Player player = (Player) area.GetScript();
				player.HealthComponent.TakeDamage(20);
			}
        }
    }
}
