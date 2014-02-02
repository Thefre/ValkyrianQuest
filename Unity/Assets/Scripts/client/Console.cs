using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
	private static Vector2 scrollPosition;
	public static string message = "";

	private static bool autoScroll = true;

	public delegate void ConsoleMessage(string inputText);
	public static event ConsoleMessage OnConsoleInput;

	// Use this for initialization
	void Start () {
		message = "Console started.";
		Bind("devConsole", 0, 0, 250, 120);
	}

	public static void Bind(string state, float xoffset, float yoffset, float width, float height) {
		Window newWindow = new Window();
		newWindow.Initialize(state, xoffset, yoffset, width, height, DevConsole, Window.Align.BottomRight);
		GUIManager.windows.Add(newWindow);
	}

	public static void Write(string txt) {
		message += "\n"+txt;
		scrollPosition.y += Mathf.Infinity;
	}
	
	public static void DevConsole() {
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		GUILayout.Label(message);
		GUILayout.EndScrollView();
	}
}
