using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class NetworkingMain : Node
{

	enum Status
	{
		Peer,
		Host,
	}


	int Port = 6969;
	public void CreateClient()
	{
		string IPAddress = "69.74.45.68";
		
		GetTree().GetMultiplayer();
		var peer = new ENetMultiplayerPeer();
		peer.CreateClient(IPAddress, Port);
		Multiplayer.MultiplayerPeer = peer;
		GetNode<Label>("isHost").Text = "IsServer: " + Multiplayer.IsServer();
		GetNode<Label>("id").Text = "ID: " + Multiplayer.GetUniqueId();
	}

	public void CreateServer()
	{
		
		int MaxClients = 1;
		var peer = new ENetMultiplayerPeer();
		peer.CreateServer(Port, MaxClients);
		Multiplayer.MultiplayerPeer = peer;
		GetNode<Label>("isHost").Text = "IsServer: " + Multiplayer.IsServer();
		GetNode<Label>("id").Text = "ID: " + Multiplayer.GetUniqueId();
		
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		string IPAddress = "127.0.0.1";
		int Port = 6969;
		GetTree().GetMultiplayer();
		var peer = new ENetMultiplayerPeer();
		peer.CreateClient(IPAddress, Port);
		Multiplayer.MultiplayerPeer = peer;
	}
	public void _on_client_pressed()
    {
        CreateClient();
    }

	public void _on_host_pressed()
    {
        CreateServer();
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
