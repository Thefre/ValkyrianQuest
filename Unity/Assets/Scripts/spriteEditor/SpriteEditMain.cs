using UnityEngine;
using System.Collections;

public class SpriteEditMain : MonoBehaviour {
	public Renderer canvas;
	private Texture2D canvasTex;
	private Texture2D selectedColor;
	private GUIStyle selectedColorStyle;

	private RGBColor mainColor;
	private bool isDrawing;

	// Use this for initialization
	void Start () {
		selectedColor = new Texture2D(32,32);
		selectedColorStyle = new GUIStyle();
		NewCanvas();
		Console.Bind("spriteEdit", 0, 0, 250, 120);
		Window newWindow = new Window();
		newWindow.Initialize("spriteEdit", 0, 0, 120, 25, Toolbar, Window.Align.TopLeft);
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("spriteEdit", 0, 0, 290, 90, Palette, Window.Align.BottomLeft);
		GUIManager.windows.Add(newWindow);
		mainColor = new RGBColor();
		mainColor.SetColor(0,0,0);

		selectedColorStyle.normal.background = selectedColor;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			return;
		
		Renderer renderer = hit.collider.renderer;
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
			return;

		if (Input.GetKey(KeyCode.LeftAlt)) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				Texture2D tex = renderer.material.mainTexture as Texture2D;
				Vector2 pixelUV = hit.textureCoord;
				Color targetPixel = new Color();

				pixelUV.x *= tex.width;
				pixelUV.y *= tex.height;

				targetPixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
				mainColor.SetColor((int)(targetPixel.r*255.0f),(int)(targetPixel.g*255.0f),(int)(targetPixel.b*255.0f));
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				isDrawing = true;
			} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
				isDrawing = false;
			}
		}
		if (isDrawing) {
			Texture2D tex = renderer.material.mainTexture as Texture2D;
			Vector2 pixelUV = hit.textureCoord;
			pixelUV.x *= tex.width;
			pixelUV.y *= tex.height;
			tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, mainColor.GetColor());
			tex.Apply();
		}
	}

	public void NewCanvas() {
		canvasTex = new Texture2D(32,32,TextureFormat.ARGB32,false);
		canvasTex.anisoLevel = 1;
		canvasTex.filterMode = FilterMode.Point;
		canvasTex.wrapMode = TextureWrapMode.Clamp;
		FillTexture(canvasTex, new Color32(0,0,0,0));
		canvas.material.mainTexture = canvasTex;
	}

	public void FillTexture(Texture2D texty, Color32 color) {
		for (int y = 0; y < texty.height; ++y)
		{
			for (int x = 0; x < texty.width; ++x)
			{
				texty.SetPixel(x, y, color);
			}
		}
		texty.Apply();
	}

	private void Toolbar() {
		GUIManager.NewRow();
		if (GUILayout.Button("New"))
			NewCanvas();
		GUIManager.EndRow();
	}

	private void Palette() {
		GUIManager.NewRow();
		GUILayout.Box("",selectedColorStyle,GUILayout.Width(64),GUILayout.Height(64));
		GUILayout.BeginVertical();
		GUIManager.NewRow();
		GUILayout.Label("Red",GUILayout.MaxWidth(50));
		mainColor.red = (int) GUILayout.HorizontalSlider(mainColor.red,0,255);
		GUIManager.NextRow();
		GUILayout.Label("Green",GUILayout.MaxWidth(50));
		mainColor.green = (int) GUILayout.HorizontalSlider(mainColor.green,0,255);
		GUIManager.NextRow();
		GUILayout.Label("Blue",GUILayout.MaxWidth(50));
		mainColor.blue = (int) GUILayout.HorizontalSlider(mainColor.blue,0,255);
		GUIManager.EndRow();
		GUILayout.EndVertical();
		GUIManager.EndRow();
		FillTexture(selectedColor, mainColor.GetColor());
	}
}
