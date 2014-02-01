using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	private const string typeName = "TKValkyrianArena";
	private string gameName = "My Game";
	private string gamePassword = "";
	private string joinIP = "localhost";
	private int gamePort = 25000;
	private string portString = "25000";
	private int playerLimit = 6;
	private bool isPrivate = false;

	private HostData[] hostList;

	public bool showMenu = false;

	public delegate void EnterGame();
	public static event EnterGame OnEnterGame;
	public delegate void ServerHostAction();
	public static event ServerHostAction OnServerHost;
	public delegate void JoinServerAction();
	public static event JoinServerAction OnJoinServer;
	public delegate void PlayerJoinAction(NetworkPlayer player);
	public static event PlayerJoinAction OnPlayerJoin;
	public delegate void PlayerLeaveAction(NetworkPlayer player);
	public static event PlayerLeaveAction OnPlayerLeave;

	private void StartServer() {
		if(gamePassword != "")
			Network.incomingPassword = gamePassword;
		Network.InitializeServer(playerLimit, gamePort, !Network.HavePublicAddress());
		if(!isPrivate)
			MasterServer.RegisterHost(typeName, gameName);
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		if(OnPlayerLeave != null) //Trigger OnPlayerLeave
			OnPlayerLeave(player);
	}

	void OnServerInitialized() {
		if(OnServerHost != null) //Trigger OnServerHost
			OnServerHost();
		if(OnEnterGame != null) //Trigger OnEnterGame
			OnEnterGame();
		Debug.Log("Server Initialized.");
	}

	private void RefreshHostList() {
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	private void JoinPrivateServer(string ipAddress, int port, string password)
	{
		if(password != "") {
			Network.Connect(ipAddress,port,password);
		} else {
			Network.Connect(ipAddress,port);
		}
	}
	
	void OnConnectedToServer()
	{
		if(OnJoinServer != null) //Trigger OnJoinServer
			OnJoinServer();
		if(OnEnterGame != null) //Trigger OnEnterGame
			OnEnterGame();
		Debug.Log("Server Joined");
	}
	
	void OnPlayerConnected(NetworkPlayer player)
	{
		if(OnPlayerJoin != null) //Trigger OnPlayerJoin
			OnPlayerJoin(player);
		Debug.Log("Player Connected");
	} 

	void Start() {
		Window newWindow = new Window();
		newWindow.Initialize("networkMenu", 0, 0, 120, 100, ShowMainMenu, Window.Align.Center, "Network Game");
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("hostMenu", 0, 0, 220, 150, ShowHostMenu, Window.Align.Center, "Host Game");
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("joinPublicMenu", 0, 0, 120, 150, ShowJoinPublic, Window.Align.Center, "Join Game");
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("joinDirectMenu", 0, 0, 220, 150, ShowJoinPrivate, Window.Align.Center, "Direct Connect");
		GUIManager.windows.Add(newWindow);
	}

	public void ShowMainMenu() {
		if(GUILayout.Button("Host Game"))
			GUIManager.state = "hostMenu";
		if(GUILayout.Button("Join Public")) {
			RefreshHostList();
			GUIManager.state = "joinPublicMenu";
		}
		if(GUILayout.Button("Join Direct"))
			GUIManager.state = "joinDirectMenu";
		if(GUILayout.Button("Main Menu"))
			GUIManager.state = "main";

	}

	public void ShowHostMenu() {
		GUIManager.NewRow();
		GUILayout.Label("Game Name:");
		gameName = GUILayout.TextField(gameName);
		GUIManager.NextRow();
		GUILayout.Label("Game Port:");
		gamePort = int.Parse(GUILayout.TextField(gamePort.ToString()));
		GUILayout.Space(10);
		isPrivate = GUILayout.Toggle(isPrivate,"Private");
		GUIManager.NextRow();
		GUILayout.Label("Password:");
		gameName = GUILayout.PasswordField(gameName,"*"[0]);
		GUIManager.NextRow();
		if(GUILayout.Button("Create"))
			StartServer();
		if(GUILayout.Button("Cancel"))
			GUIManager.state = "networkMenu";
		GUIManager.EndRow();
	}

	public void ShowJoinPublic() {
		if (hostList != null)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				if (GUILayout.Button(hostList[i].gameName))
					JoinServer(hostList[i]);
			}
		}
		GUIManager.NewRow();
		if (GUILayout.Button("Refresh"))
			RefreshHostList();
		if (GUILayout.Button("Cancel"))
			GUIManager.state = "networkMenu";
		GUIManager.EndRow();
	}

	public void ShowJoinPrivate() {
		GUIManager.NewRow();
		GUILayout.Label("IP Address:");
		joinIP = GUILayout.TextField(joinIP);
		GUIManager.NextRow();
		GUILayout.Label("Game Port:");
		gamePort = int.Parse(GUILayout.TextField(gamePort.ToString()));
		GUIManager.NextRow();
		GUILayout.Label("Password:");
		gameName = GUILayout.PasswordField(gameName,"*"[0]);
		GUIManager.NextRow();
		if (GUILayout.Button("Join"))
			JoinPrivateServer(joinIP,gamePort,gamePassword);
		if (GUILayout.Button("Cancel"))
			GUIManager.state = "networkMenu";
		GUIManager.EndRow();
	}
}
