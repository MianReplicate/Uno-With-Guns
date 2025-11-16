using Godot;
using System;

public partial class Bullet : Area3D
{
	// public 
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

	public void OnBodyEntered(Node3D body)
    {
		if(body != Shooter)
        {
			this.QueueFree();
			GD.Print("Touched someone!");
			if(body is CharacterBody3D) // should be the receiver bullet
			{
				GD.Print("Hit by bullet!");
				Player.INSTANCE.HealthComponent.Health -= 20;
			}
        }
    }
}
