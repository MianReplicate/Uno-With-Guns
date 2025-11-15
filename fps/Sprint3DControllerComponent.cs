using Godot;

public partial class Sprint3DControllerComponent : Node
{
    public bool IsSprint = false;

    public override void _PhysicsProcess(double delta)
    {
        IsSprint = Input.IsActionPressed("sprint");
    }
}
