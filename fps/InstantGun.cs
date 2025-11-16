using Godot;
using System;

public partial class InstantGun : Node3D
{
    // [Export] public PackedScene Bullet;
    private PackedScene _bullet;
	[Export] public AudioStreamPlayer3D AudioStreamer;

    public override void _Ready()
    {
        _bullet = GD.Load<PackedScene>("res://Bullet.tscn");
    }

	public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
			if(mouseEvent.IsPressed() && Input.IsMouseButtonPressed(MouseButton.Left))
            	Shoot();
        }
    }

	public void Shoot()
    {
        AudioStreamer.Play();

        Bullet bulletScene = (Bullet) _bullet.Instantiate();
        bulletScene.InitialPosition = this.GlobalPosition;
        bulletScene.InitialRotation = ((Node3D)this.GetParent()).GlobalRotation;
        // var bullet = (Bullet) bulletScene.GetChild(0).GetScript();
        bulletScene.Shooter = (CharacterBody3D) GetParent().GetParent();

        this.GetParent().GetParent().GetParent().AddChild(bulletScene);

    }
}
