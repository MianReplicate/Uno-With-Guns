using Godot;
using System;
using System.Numerics;

public partial class Gunshot : AudioStreamPlayer3D
{
	public Godot.Vector3 StartPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		this.Position = StartPosition;
        this.Play();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void OnFinish()
    {
        this.QueueFree();
    }
}
