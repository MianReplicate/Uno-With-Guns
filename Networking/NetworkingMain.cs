using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class NetworkingMain : CanvasLayer
{

	enum Status
	{
		Peer,
		Host,
	}


	int Port = 6969;

	// Timer used to poll the connection status until it's connected
	private Timer _connectionTimer;
	public void CreateClient()
	{
		string IPAddress = "127.0.0.1";
		
		GD.Print($"[CreateClient] Attempting to connect to {IPAddress}:{Port}");
		GetTree().GetMultiplayer();
		var peer = new ENetMultiplayerPeer();
		var error = peer.CreateClient(IPAddress, Port);
		GD.Print($"[CreateClient] CreateClient returned: {error}");
		
		Multiplayer.MultiplayerPeer = peer;
		GetNode<Label>("isHost").Text = "IsServer: " + Multiplayer.IsServer();
		GetNode<Label>("id").Text = "ID: " + Multiplayer.GetUniqueId();

		StartConnectionTimer();
	}

	public void CreateServer()
	{
		GD.Print($"[CreateServer] Starting server on port {Port}");
		int MaxClients = 2;
		var peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(Port, MaxClients);
		GD.Print($"[CreateServer] CreateServer returned: {error}");
		
		Multiplayer.MultiplayerPeer = peer;
		GetNode<Label>("isHost").Text = "IsServer: " + Multiplayer.IsServer();
		GetNode<Label>("id").Text = "ID: " + Multiplayer.GetUniqueId();
		
		GD.Print("[CreateServer] Server is running. Waiting for clients...");
		StartConnectionTimer();
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Don't auto-connect in _Ready. Let the user click HOST or CLIENT buttons instead.
		GD.Print("[_Ready] NetworkingMain initialized. Press HOST or CLIENT to connect.");

		// Defer button disable to ensure the scene is fully ready
		Callable.From(() =>
		{
			try
			{
				var sendBtn = GetNode<Button>("sendpacket");
				if (sendBtn != null)
					sendBtn.Disabled = true;
			}
			catch (Exception ex)
			{
				GD.PrintErr($"Error disabling send button: {ex}");
			}
		}).CallDeferred();
	}
	public void _on_client_pressed()
	{
		CreateClient();
	}

	public void _on_host_pressed()
	{
		CreateServer();
	}

	public void _on_sendpacket_pressed()
	{
		// Don't try to RPC if we don't have a connected multiplayer peer
		if (!IsPeerConnected())
		{
			GD.PrintErr("Cannot send RPC: multiplayer peer is not connected.");
			return;
		}

		Rpc(nameof(killyourself));
	}

	private bool IsPeerConnected()
	{
		var mp = Multiplayer.MultiplayerPeer;
		if (mp == null)
		{
			GD.PrintErr("[IsPeerConnected] Multiplayer.MultiplayerPeer is null");
			return false;
		}
		// If this is an ENet peer we can check the connection status
		if (mp is ENetMultiplayerPeer enet)
		{
			try
			{
				var status = enet.GetConnectionStatus();
				GD.Print($"[IsPeerConnected] ENet status: {status}");
				bool isConnected = status == ENetMultiplayerPeer.ConnectionStatus.Connected;
				return isConnected;
			}
			catch (Exception ex)
			{
				GD.PrintErr($"[IsPeerConnected] Exception checking ENet status: {ex}");
				return false;
			}
		}
		GD.PrintErr("[IsPeerConnected] Multiplayer peer is not ENetMultiplayerPeer");
		return true;
	}

	private void StartConnectionTimer()
	{
		// If already running, do nothing
		if (_connectionTimer != null && _connectionTimer.IsInsideTree())
			return;

		_connectionTimer = new Timer();
		_connectionTimer.WaitTime = 0.2f;
		_connectionTimer.OneShot = false;
		AddChild(_connectionTimer);
		_connectionTimer.Timeout += OnConnectionTimerTimeout;
		_connectionTimer.Start();
	}

	private void OnConnectionTimerTimeout()
	{
		if (IsPeerConnected())
		{
			// Enable send button and stop polling
			try
			{
				var sendBtn = GetNode<Button>("sendpacket");
				if (sendBtn != null)
					sendBtn.Disabled = false;
			}
			catch (Exception ex)
			{
				GD.PrintErr($"Error enabling send button: {ex}");
			}

			if (_connectionTimer != null)
			{
				_connectionTimer.Stop();
				_connectionTimer.Timeout -= OnConnectionTimerTimeout;
				_connectionTimer.QueueFree();
				_connectionTimer = null;
			}
			GD.Print("Multiplayer peer connected. Send enabled.");
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	public void killyourself()
	{
		GD.Print("Hello worldadfwjifhaudigfiulsdftgyutsdgftut");
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
