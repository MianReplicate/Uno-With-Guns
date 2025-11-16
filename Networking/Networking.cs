using Godot;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class Networking : CanvasLayer
{

	public static Networking Instance;

	enum Status
	{
		Peer,
		Host,
	}


	int Port = 6969;

	public Node3D Op => GetParent().GetNode("Map").GetNode<Node3D>("Theop");

	[Export]
	private int NumPlayersConnected = 0; 
	[Export]
	public CharacterBody3D Player ;

	public bool IsHost => Multiplayer.IsServer();
	// Timer used to poll the connection status until it's connected
	private Timer _connectionTimer;

		public void CreateClient(int port, string address)
	{
		Port = port;
		
		GD.Print($"[CreateClient] Attempting to connect to {address}:{Port}");
		GetTree().GetMultiplayer();
		var peer = new ENetMultiplayerPeer();
		var error = peer.CreateClient(address, Port);
		GD.Print($"[CreateClient] CreateClient returned: {error}");
		
		Multiplayer.MultiplayerPeer = peer;

		StartConnectionTimer();
	}

	public void CreateServer(int port)
	{
		Port = port;

		GD.Print($"[CreateServer] Starting server on port {Port}");
		int MaxClients = 2;
		var peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(Port, MaxClients);
		GD.Print($"[CreateServer] CreateServer returned: {error}");
		
		Multiplayer.MultiplayerPeer = peer;
		
		GD.Print("[CreateServer] Server is running. Waiting for clients...");
		StartConnectionTimer();
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		// Don't auto-connect in _Ready. Let the user click HOST or CLIENT buttons instead.
		GD.Print("[_Ready] NetworkingMain initialized. Press HOST or CLIENT to connect.");


	}
	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
		public void CreateBullet(string bulletRes, Vector3 position, Vector3 rotation)
	{
		Helper.INSTANCE.CreateBullet(bulletRes, position, rotation, null, true);
	}

	public void StartGunGame()
	{
		Op.Visible = true;
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	public void OtherPlayerDied()
	{
		Op.Visible = false;
				Multiplayer.MultiplayerPeer.Close();
		GetTree().ChangeSceneToFile("res://Networking/networking_main.tscn");
	}

	public void TrySendPacket(string packetName, params Variant[] args)
	{
		// Don't try to RPC if we don't have a connected multiplayer peer
		if (!IsPeerConnected())
		{
			GD.PrintErr("Cannot send RPC: multiplayer peer is not connected.");
			return;
		}

		Rpc(packetName, args);
	}
	public bool IsPeerConnected()
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
				bool isConnected = status == ENetMultiplayerPeer.ConnectionStatus.Connected;
				return isConnected;
			}
			catch (Exception ex)
			{
				GD.PrintErr($"[IsPeerConnected] Exception checking ENet status: {ex}");
				return false;
			}
		}
		// GD.PrintErr("[IsPeerConnected] Multiplayer peer is not ENetMultiplayerPeer");
		return false;
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
	public void UpdateOpPosition(Vector3 newPos)
	{
		Op.GlobalPosition = newPos;
		Op.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
