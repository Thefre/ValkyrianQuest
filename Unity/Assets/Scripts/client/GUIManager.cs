using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public static string UImode;
	public static List<Window> windows = new List<Window>();

	void OnGUI() {
		foreach (Window w in windows) {
			w.Draw();
		}
	}
}
