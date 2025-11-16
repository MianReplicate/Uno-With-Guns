using Godot;
using System;

public partial class Theop : Node3D
{
	float OpTimer = 0;
	float OpUpdateWaitTime = 1; // frames
	public override void _Ready()
    {
        GetNode<AnimatedSprite3D>("AnimatedSprite3D").Play("default");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
    {
        OpTimer += 1;

		if(OpTimer > OpUpdateWaitTime && Player.INSTANCE != null)
        {
            OpTimer = 0;
			NetworkingMain.INSTANCE.TrySendPacket("UpdateOpPosition", Player.INSTANCE.GlobalPosition);

        }

    }
}
