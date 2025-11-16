using Godot;
using System;

public partial class NetworkingUI : CanvasLayer
{

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

	public bool clientToCreate = true;
		public void _on_client_pressed()
    {
		clientToCreate = true;
    }

	public void _on_host_pressed()
    {
		clientToCreate = false;
    }

	public void _on_play_pressed()
    {
		this.Visible = false;
		int port = GetNode<LineEdit>("port").Text.ToInt();
		string address = GetNode<LineEdit>("ip").Text.ToString();
        if(clientToCreate)
			Networking.Instance.CreateClient(port, address);
		else
			Networking.Instance.CreateServer(port);
		GetTree().ChangeSceneToFile("res://GunGame.tscn");
	}
}
