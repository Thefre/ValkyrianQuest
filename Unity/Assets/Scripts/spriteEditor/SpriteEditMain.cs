using UnityEngine;
using System.Collections;

public class SpriteEditMain : MonoBehaviour {
	public Renderer canvas;
	private Texture2D canvasTex;
	private Texture2D selectedColor;
	private GUIStyle selectedColorStyle;
	private Texture2D[] paletteColors = new Texture2D[32];
	private GUIStyle[] paletteStyles = new GUIStyle[32];

	private SpriteSheetData sheetData;

	private RGBColor mainColor;
	private bool isDrawing;
	private int currentColor = 1;

	// Use this for initialization
	void Start () {
		selectedColor = new Texture2D(32,32);
		selectedColorStyle = new GUIStyle();

		sheetData = new SpriteSheetData();
		sheetData.Initialize();
		SetPalettes();
		canvas.material.mainTexture = sheetData.buffered;
		//NewCanvas();

		//GUI Elements
		Console.Bind("spriteEdit", 0, 0, 250, 120);
		Window newWindow = new Window();
		newWindow.Initialize("spriteEdit", 0, 0, 120, 25, Toolbar, Window.Align.TopLeft);
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("spriteEdit", 0, 0, 290, 90, Palette, Window.Align.BottomLeft);
		GUIManager.windows.Add(newWindow);
		newWindow = new Window();
		newWindow.Initialize("spriteEdit", 0, 0, 290, 90, Swatches, Window.Align.Bottom);
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

		if (hit.transform.gameObject.name == "Canvas") {
			Texture2D tex = renderer.material.mainTexture as Texture2D;
			Vector2 pixelUV = hit.textureCoord;
			Color targetPixel = new Color();
			
			pixelUV.x *= tex.width;
			pixelUV.y *= tex.height;
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				if (Input.GetKey(KeyCode.LeftAlt)) {
					
					
					targetPixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
					
					sheetData.SetPaletteColor(currentColor, targetPixel);
					//				mainColor.SetColor((int)(targetPixel.r*255.0f),(int)(targetPixel.g*255.0f),(int)(targetPixel.b*255.0f));
				} else {
					isDrawing = true;
				}	
			} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
				isDrawing = false;
			}
			
			if (isDrawing) {
				sheetData.Draw((int)pixelUV.x, (int)pixelUV.y, currentColor);
			}
		}
	}

	public void NewCanvas() {
		canvasTex = new Texture2D(32,32,TextureFormat.ARGB32,false);
		canvasTex.anisoLevel = 1;
		canvasTex.filterMode = FilterMode.Point;
		canvasTex.wrapMode = TextureWrapMode.Clamp;
		FillTexture(canvasTex, new Color(0,0,0,0));
		canvas.material.mainTexture = canvasTex;
	}

	public void FillTexture(Texture2D texty, Color color) {
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
		sheetData.SetPaletteColor(currentColor, mainColor.GetColor());
	}

	private void SetPalettes() {
		for (int p = 0; p < paletteStyles.Length; p++) {
			paletteColors[p] = new Texture2D(16,16);
			FillTexture(paletteColors[p], sheetData.GetPaletteColor(p+1));
			paletteStyles[p] = new GUIStyle();
			paletteStyles[p].normal.background = paletteColors[p];
		}
	}

	private void Swatches() {
		GUIManager.NewRow();
		for (int y = 0; y < 4; ++y)
		{
			for (int x = 0; x < 8; ++x)
			{
				if (GUILayout.Button("",paletteStyles[x+(y*8)],GUILayout.Width(16),GUILayout.Height(16))){
					currentColor = x+1+(y*8);
					Debug.Log(currentColor);
				}
			}
			if (y == 3)
				GUIManager.EndRow();
			else
				GUIManager.NextRow();
		}

	}
}
