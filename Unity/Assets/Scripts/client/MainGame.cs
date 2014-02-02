using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
	public TKFPS fps;
	public GUIText fpsText;

	public SpriteData[] spriteData;

	private Vector2 scrollPosition;
	public string message = "Console started.";

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = -1;

		// Assign Menus
		Window newWindow = new Window();
		newWindow.Initialize("main", 0, 0, 120, 120, MainMenu, Window.Align.Center, "Main Menu");
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("devConsole", 0, 0, 250, 120, DevConsole, Window.Align.BottomRight);
		GUIManager.windows.Add(newWindow);
	}
	
	// Update is called once per frame
	void Update () {
		fps.PrintGUIText(fpsText);

		if(Input.GetKeyDown(KeyCode.Space)) {
			GUIManager.state = "main";
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			message += "\nTest";
		}
	}

	public void MainMenu() {
		if(GUILayout.Button("Multiplayer"))
			GUIManager.state = "networkMenu";
		if(GUILayout.Button("Anim Test"))
			GUIManager.state = "animTest";
		if(GUILayout.Button("Console"))
			GUIManager.state = "devConsole";
	}

	public void DevConsole() {
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(240), GUILayout.Height(110));
		GUILayout.Label(message);
		GUILayout.EndScrollView();
	}
}
