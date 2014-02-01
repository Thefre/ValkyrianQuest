using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
	public TKFPS fps;
	public GUIText fpsText;

	public SpriteData[] spriteData;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = -1;

		// Assign Menus
		Window newWindow = new Window();
		newWindow.Initialize("main", 0, 0, 120, 100, MainMenu, Window.Align.Center, "Main Menu");
		GUIManager.windows.Add(newWindow);

	}
	
	// Update is called once per frame
	void Update () {
		fps.PrintGUIText(fpsText);

		if(Input.GetKeyDown(KeyCode.Space)) {
			GUIManager.state = "main";
		}
	}

	public void MainMenu() {
		if(GUILayout.Button("Multiplayer"))
			GUIManager.state = "networkMenu";
		if(GUILayout.Button("Anim Test"))
			GUIManager.state = "animTest";

	}

}
