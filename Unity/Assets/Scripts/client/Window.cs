using UnityEngine;
using System.Collections;

public class Window {
	public enum Align {
		TopLeft,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight
	}
	public Align alignment;
	public string state;
	private float _xbase;
	private float _xmod;
	private float _xoffset;
	private float _ybase;
	private float _ymod;
	private float _yoffset;
	public float x {
		get {
			return _xbase + (_xoffset * _xmod);
		}
		set {
			_xoffset = value;
		}
	}
	public float y {
		get {
			return _ybase + (_yoffset * _ymod);
		}
		set {
			_yoffset = value;
		}
	}
	public float width;
	public float height;
	public string title = "";
	public delegate void BodyContent();
	public BodyContent Body;

	public void Initialize(string setState, float setX, float setY, float setWidth, float setHeight, BodyContent setBody, Align align = Align.Center, string setTitle = "") {
		alignment = align;
		state = setState;
		switch (alignment) {
		case Align.TopLeft:
			_xbase = 0;
			_xmod = 1;
			_ybase = 0;
			_ymod = 1;
			break;
		case Align.Top:
			_xbase = (Screen.width-setWidth)/2;
			_xmod = 1;
			_ybase = 0;
			_ymod = 1;
			break;
		case Align.TopRight:
			_xbase = Screen.width-setWidth;
			_xmod = -1;
			_ybase = 0;
			_ymod = 1;
			break;
		case Align.Left:
			_xbase = 0;
			_xmod = 1;
			_ybase = (Screen.height-setHeight)/2;
			_ymod = 1;
			break;
		case Align.Right:
			_xbase = Screen.width-setWidth;
			_xmod = -1;
			_ybase = (Screen.height-setHeight)/2;
			_ymod = 1;
			break;
		case Align.Center:
			_xbase = (Screen.width-setWidth)/2;
			_xmod = 1;
			_ybase = (Screen.height-setHeight)/2;
			_ymod = 1;
			break;
		case Align.BottomLeft:
			_xbase = 0;
			_xmod = 1;
			_ybase = Screen.height-setHeight;
			_ymod = -1;
			break;
		case Align.BottomRight:
			_xbase = Screen.width-setWidth;
			_xmod = -1;
			_ybase = Screen.height-setHeight;
			_ymod = -1;
			break;
		case Align.Bottom:
			_xbase = (Screen.width-setWidth)/2;
			_xmod = 1;
			_ybase = Screen.height-setHeight;
			_ymod = -1;
			break;
		}

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
