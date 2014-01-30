using UnityEngine;
using System.Collections;

public class Window {
	public float x;
	public float y;
	public float width;
	public float height;
	public string title = "";
	public delegate void BodyContent();
	public BodyContent Body;

	public void Initialize(float setX, float setY, float setWidth, float setHeight, BodyContent setBody, string setTitle = "") {
		x = setX;
		y = setY;
		width = setWidth;
		height = setHeight;
		title = setTitle;
		Body = setBody;
	}

	public void Draw() {
		GUI.Box(new Rect(x,y,width,height),title);
		if (title != "")
			GUILayout.BeginArea(new Rect(x+5,y+30,width-10,height-35));
		else
			GUILayout.BeginArea(new Rect(x+5,y+5,width-10,height-10));
		if(Body != null)
			Body();
		GUILayout.EndArea();
	}
}
