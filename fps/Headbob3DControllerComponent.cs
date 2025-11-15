using Godot;

[GlobalClass]
public partial class Headbob3DControllerComponent : Node
{
    [Export] public Sprint3DControllerComponent Sprint3DControllerComponent;

    private float headbobSpeed = 6.0f;
    private float headbobTimer = Mathf.Pi; 
    private float headbobDistance = 0.07f;

    public Vector3 Headbob(float delta, Vector2 inputDir)
    {
        if (inputDir != Vector2.Zero)
        {
            float sprintMultiplier = Sprint3DControllerComponent.IsSprint ? 2f : 1f;
            headbobTimer += delta * headbobSpeed / 2.0f * sprintMultiplier;

            if (headbobTimer > Mathf.Pi * 2f)
                headbobTimer = 0f;
        }
        else
        {
            if (headbobTimer <= Mathf.Pi)
                headbobTimer += delta * headbobSpeed * 2.0f;
        }

        float sprintScale = Sprint3DControllerComponent.IsSprint ? 1.3f : 1.0f;

        Vector3 headbobPosition = Vector3.Zero;
        headbobPosition.Y = Mathf.Sin(headbobTimer * 2f) * headbobDistance * sprintScale;
        headbobPosition.X = Mathf.Cos(headbobTimer) * headbobDistance * sprintScale;

        return headbobPosition;
    }
}
