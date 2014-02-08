using UnityEngine;
using System.Collections;

public class SpriteSheetData {
	public int tileSize;
	public int sheetWidth;
	public int sheetHeight;
	public int palette;

	public Texture2D baseSheet;
	public Texture2D colorPalettes;
	public Texture2D coloredSheet;

	public Texture2D buffered {
		get {
			return coloredSheet;
		}
	}

	public void Initialize() {
		ResetPalettes();
		NewSheets(32,1,1);
		palette = 2;
	}

	public void NewSheets(int tile, int width, int height) {
		baseSheet = new Texture2D(tile*width, tile*height,TextureFormat.ARGB32,false);
		coloredSheet = new Texture2D(tile*width, tile*height,TextureFormat.ARGB32,false);
		for (int x = 0; x < baseSheet.width; ++x) {
			for (int y = 0; y < baseSheet.height; ++y) {
				baseSheet.SetPixel(x,y,new Color(0,0,0,0));
				coloredSheet.SetPixel(x,y,new Color(0,0,0,0));
			}
		}
		coloredSheet.anisoLevel = 1;
		coloredSheet.filterMode = FilterMode.Point;
		coloredSheet.wrapMode = TextureWrapMode.Clamp;
		coloredSheet.Apply();
	}

	public void ResetPalettes() {
		colorPalettes = new Texture2D(32,32);
		for (int x = 0; x < 32; x++) {
			for (int y = 0; y < 32; y++) {
				if(y==0) {
					colorPalettes.SetPixel(x+1,y+1,new Color((float)x,(float)x,(float)x,1));

				} else {
					colorPalettes.SetPixel(x+1,y+1,new Color((((float)x*7)/255),(((float)x*7)/255),(((float)x*7)/255),1));
					Debug.Log(colorPalettes.GetPixel(x+1,y+1));
				}
			}
		}
		colorPalettes.Apply();
	}

	public void SetPaletteColor(int colorID, Color color) {
		for (int x = 0; x < baseSheet.width; x++) {
			for (int y = 0; y < baseSheet.height; y++) {
				if(baseSheet.GetPixel(x,y) == colorPalettes.GetPixel(colorID,1) && coloredSheet.GetPixel(x,y) != colorPalettes.GetPixel(colorID,palette)) {
					coloredSheet.SetPixel(x,y,color);
				}
			}
		}
		coloredSheet.Apply();
		colorPalettes.SetPixel(colorID,palette,color);
		colorPalettes.Apply();
	}

	public Color GetPaletteColor(int colorID) {
		return colorPalettes.GetPixel(colorID,palette);
	}

	public void SwapPalettes(int paletteID) {
		for (int x = 0; x < baseSheet.width; x++) {
			for (int y = 0; y < baseSheet.height; y++) {
				if(baseSheet.GetPixel(x,y).a > 0) {
					int c = (int)(baseSheet.GetPixel(x,y).r*255);
					if (colorPalettes.GetPixel(c,palette) != colorPalettes.GetPixel(c,paletteID)) {
						coloredSheet.SetPixel(x,y,colorPalettes.GetPixel(c,paletteID));
					}
				}
			}
		}
		coloredSheet.Apply();
		palette = paletteID;
	}

	public void Draw(int x, int y, int colorID) {
		baseSheet.SetPixel(x,y,colorPalettes.GetPixel(colorID,1));
		coloredSheet.SetPixel(x,y,colorPalettes.GetPixel(colorID,palette));
		baseSheet.Apply();
		coloredSheet.Apply();
	}
}
