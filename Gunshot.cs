using Godot;
using System;

public partial class Gunshot : AudioStreamPlayer3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        this.Play();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void OnFinish()
    {
        this.QueueFree();
    }
}
