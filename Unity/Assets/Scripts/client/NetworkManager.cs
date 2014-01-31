using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	private enum NetworkState {
		MainMenu,
		HostMenu,
		JoinPublic,
		JoinPrivate
	}

	private NetworkState netState = NetworkState.MainMenu;
	private const string typeName = "TKValkyrianArena";
	private string gameName = "My Game";
	private string gamePassword = "";
	private string joinIP = "localhost";
	private int gamePort = 25000;
	private string portString = "25000";
	private int playerLimit = 4;
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
	/*
	void Start() {

	}

	public void ShowMainMenu() {
		GUI.Box(new Rect(new Rect(Menu.x-10, Menu.row, 170, (Menu.rowHeight*6)+10)),"Multiplayer");
		if (GUI.Button(new Rect(Menu.x, Menu.row, 150, Menu.rowHeight), "Host Game")) {
			netState = NetworkState.HostMenu;
		}
		if (GUI.Button(new Rect(Menu.x, Menu.row, 150, Menu.rowHeight), "Join Public")) {
			RefreshHostList();
			netState = NetworkState.JoinPublic;
		}
		if (GUI.Button(new Rect(Menu.x, Menu.row, 150, Menu.rowHeight), "Join Direct"))
			netState = NetworkState.JoinPrivate;
		if (GUI.Button(new Rect(Menu.x, Menu.row, 150, Menu.rowHeight), "Main Menu"))
			showMenu = false;
	}

	public void ShowHostMenu() {
		GUI.Box(new Rect(new Rect(Menu.x-10, Menu.row, 250, (Menu.rowHeight*7)+10)),"Host Game");
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"Game Name:");
		gameName = GUI.TextField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),gameName);
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"Game Port:");
		portString = GUI.TextField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),portString.ToString());
		int num = 0;
		if (int.TryParse(portString, out gamePort))
		{
			num = Mathf.Clamp(gamePort, 5000, 48000);
		}
		else if (portString == "") num = 0;
		gamePort = num;
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"Password:");
		gamePassword = GUI.PasswordField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),gamePassword,"*"[0]);
		isPrivate = GUI.Toggle(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),isPrivate,"Private");
		if (GUI.Button(new Rect(Menu.x+40, Menu.rowS, 80, Menu.rowHeight), "Create")) {
			StartServer();
		}
		if (GUI.Button(new Rect(Menu.x+125, Menu.row, 80, Menu.rowHeight), "Cancel")) {
			netState = NetworkState.MainMenu;
		}
	}

	public void ShowJoinPublic() {
		if (hostList != null)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				if (GUI.Button(new Rect(Menu.x, Menu.row, 150, 25), hostList[i].gameName))
					JoinServer(hostList[i]);
			}
		}
		if (GUI.Button(new Rect(Screen.width-170, Screen.height-(Menu.rowHeight+5), 80, Menu.rowHeight), "Refresh")) {
			RefreshHostList();
		}
		if (GUI.Button(new Rect(Screen.width-85, Screen.height-(Menu.rowHeight+5), 80, Menu.rowHeight), "Cancel")) {
			netState = NetworkState.MainMenu;
		}
	}

	public void ShowJoinPrivate() {
		GUI.Box(new Rect(new Rect(Menu.x-10, Menu.row, 250, (Menu.rowHeight*6)+10)),"Join Game");
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"IP Address:");
		joinIP = GUI.TextField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),joinIP);
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"Game Port:");
		portString = GUI.TextField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),portString.ToString());
		int num = 0;
		if (int.TryParse(portString, out gamePort))
		{
			num = Mathf.Clamp(gamePort, 5000, 48000);
		}
		else if (portString == "") num = 0;
		gamePort = num;
		GUI.Label(new Rect(new Rect(Menu.x, Menu.rowS, 80, Menu.rowHeight)),"Password:");
		gamePassword = GUI.PasswordField(new Rect(new Rect(Menu.x+85, Menu.row, 150, Menu.rowHeight)),gamePassword,"*"[0]);
		if (GUI.Button(new Rect(Menu.x+40, Menu.rowS, 80, Menu.rowHeight), "Join")) {
			JoinPrivateServer(joinIP,gamePort,gamePassword);
		}
		if (GUI.Button(new Rect(Menu.x+125, Menu.row, 80, Menu.rowHeight), "Cancel")) {
			netState = NetworkState.MainMenu;
		}
	}
*/
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
}
