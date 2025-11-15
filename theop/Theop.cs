using Godot;
using System;

public partial class Theop : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public CharacterBody3D Player ;
	float OpTimer = 0;
	float OpUpdateWaitTime = 1; // frames
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
    {
        OpTimer += 1;

		if(OpTimer > OpUpdateWaitTime)
        {
            OpTimer = 0;
			GetNode("../NetworkingMain").Call("UpdateOpShit");

        }

    }
}
