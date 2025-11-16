using Godot;
using System;

public partial class NetworkingMain : CanvasLayer
{


	// Called when the node enters the scene tree for the first time.
	public void _on_client_pressed()
	{
		Networking.Instance.clientToCreate = true;
	}

	public void _on_host_pressed()
	{
		Networking.Instance.clientToCreate = false;
	}

	public void _on_play_pressed()
	{
		// this.Visible = false;
		if (Networking.Instance.clientToCreate)
		{
			Networking.Instance.CreateClient();
		}
		else
		{
			Networking.Instance.CreateServer();
		}


		GetTree().ChangeSceneToFile("res://UNO.tscn");
	}
}
