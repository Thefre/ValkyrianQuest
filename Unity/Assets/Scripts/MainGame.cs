using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
	public TKFPS fps;
	public GUIText fpsText;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = -1;
	}
	
	// Update is called once per frame
	void Update () {
		fps.PrintGUIText(fpsText);
	}
}
