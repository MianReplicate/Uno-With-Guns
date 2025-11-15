using Godot;

public partial class FovControllerComponent : Node
{
    [Export] public Camera3D Camera;
    [Export] public Sprint3DControllerComponent Sprint3DControllerComponent;

    public void Fov(Vector2 inputDir)
    {
        float currentFov = 90f;

        // inputDir.Y != 0 means "truthy" like the GDScript version
        if (inputDir.Y != 0)
            currentFov += 6f;

        if (Sprint3DControllerComponent.IsSprint)
            currentFov += 8f;

        var tween = GetTree().CreateTween();
        tween.TweenProperty(Camera, "fov", currentFov, 0.2f);
    }
}
