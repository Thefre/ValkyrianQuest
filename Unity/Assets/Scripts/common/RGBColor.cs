using UnityEngine;
using System.Collections;

public class RGBColor
{
	public string id;
	public int red;
	public int green;
	public int blue;
	public float alpha = 1;
	public void SetColor(int r, int g, int b, float a = 1) {
		red = r;
		green = g;
		blue = b;
		alpha = a;
	}
	public Color GetColor() {
		return new Color((float)red/255,(float)green/255,(float)blue/255,alpha);
	}
}
