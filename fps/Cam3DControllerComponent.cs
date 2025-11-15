using System.Xml;
using Godot;

[GlobalClass]
public partial class Cam3DControllerComponent : Node {
	const float LOOKSENSE = 0.0025f * 4;
	
	[Export] public Sprint3DControllerComponent sprint3DControllerComponent;

	[Export] public CharacterBody3D Actor;
	[Export] public Camera3D Camera;

	[Export] public int MaxCameraDebounce = 10;

	public bool Paused = false;
	public int Debounce = 0;


	public override void _Ready()
	{
		if (Actor == null)
			Actor = (CharacterBody3D) GetParent().GetParent();

		if (Camera == null)
			Camera = (Camera3D) GetParent();
	}

    public override void _Input(InputEvent @event)
    {
		if(Input.IsKeyPressed(Key.Escape) && Debounce == 0){
			Paused = !Paused;
			Debounce = MaxCameraDebounce;
		}
		
		if(@event is InputEventMouseMotion mouseMotion && !Paused){
			Actor.RotateY(-mouseMotion.Relative.X * LOOKSENSE);
			Camera.RotateX(-mouseMotion.Relative.Y * LOOKSENSE);

			var cameraRot = Camera.Rotation;
			cameraRot.X = Mathf.Clamp(Camera.Rotation.X, -Mathf.Pi/2, Mathf.Pi/2);

			Camera.Rotation = cameraRot;
		}

    }

	public override void _PhysicsProcess(double delta){
		if(Debounce > 0)
			Debounce -= 1;
		
		updateMouseMode();
	}


	public void updateMouseMode(){
		if(Paused)
			Input.MouseMode = Input.MouseModeEnum.Visible;
		else
			Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public void cameraSway(Vector3 inputDir){
		var tween = GetTree().CreateTween();
		tween.TweenProperty(Camera, "rotation:z", ( inputDir != null ? (Mathf.DegToRad(sprint3DControllerComponent.IsSprint ? -5.5 : -2.5) * inputDir.X) : 0), 0.2);
		tween.SetParallel();
	}

}