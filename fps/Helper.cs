using System;
using Godot;

[GlobalClass]
public partial class Helper : Node
{
    public PackedScene Gunshot;
    public static Helper INSTANCE;

    public override void _Ready()
    {
        INSTANCE = this;
        Gunshot = GD.Load<PackedScene>("res://Gunshot.tscn");
    }

    public string GenerateID()
    {
        var randomGen = new RandomNumberGenerator();
        randomGen.Randomize();
        return randomGen.Randi().ToString() + randomGen.Randi().ToString() + randomGen.Randi().ToString();
    }

    public void CreateBullet(string bulletRes, Vector3 position, Vector3 rotation, CharacterBody3D shooter, bool wasNetworked=false)
    {
        PackedScene _bullet = GD.Load<PackedScene>(bulletRes);
        Bullet bulletScene = (Bullet) _bullet.Instantiate();
        bulletScene.InitialPosition = position;
        bulletScene.InitialRotation = rotation;
        bulletScene.Shooter = shooter;

        Gunshot gunScript = (Gunshot) Gunshot.Instantiate();

        gunScript.StartPosition = position;

        GetTree().CurrentScene.AddChild(bulletScene);
        GetTree().CurrentScene.AddChild(gunScript);

        if(wasNetworked)
            return;

        NetworkingMain.INSTANCE.TrySendPacket("CreateBullet", bulletRes, position, rotation);
    }
}