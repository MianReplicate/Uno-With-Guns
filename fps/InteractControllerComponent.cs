using Godot;

[GlobalClass]
public partial class InteractControllerComponent : Node
{
    private Node collider;

    [Export] public RayCast3D InteractCast;
    [Export] public Control Tooltip;

    public override void _Input(InputEvent @event)
    {
        // if (@event is InputEventKey keyEvent)
        // {
        //     // Clear old collider when appropriate
        //     if (collider != null && (InteractCast.IsColliding() || collider != InteractCast.GetCollider()))
        //     {
        //         if ((bool)collider.Get("current") == true)
        //             collider.Set("current", false);

        //         collider = null;
        //     }

        //     Node castCollider = (Node) InteractCast.GetCollider();

        //     // New collider with tooltip available
        //     if (castCollider != null && castCollider.HasMethod("get") && (string) castCollider.Get("tooltip") != null)
        //     {
        //         collider = castCollider;
        //         collider.Set("current", true);

        //         Tooltip.Set("text", collider.Get("tooltip"));

        //         bool raycastActive = (bool) collider.Get("raycast_active");

        //         if (raycastActive && keyEvent.IsActionPressed("throw_left"))
        //         {
        //             collider.Call("interact", "throw_left", GetParent());
        //         }
        //         else if (raycastActive && keyEvent.IsActionPressed("throw_right"))
        //         {
        //             collider.Call("interact", "throw_right", GetParent());
        //         }
        //     }
        //     else
        //     {
        //         Tooltip.Set("text", "");
        //     }
        // }
    }
}
