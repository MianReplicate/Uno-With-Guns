using Godot;
using System;

public partial class InstantGun : Node3D
{

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
        Helper.INSTANCE.CreateBullet(
            "res://Bullet.tscn",
            this.GlobalPosition,
            ((Node3D) this.GetParent()).GlobalRotation,
            (CharacterBody3D) GetParent().GetParent()
        );

    }
}
