using UnityEngine;
using System.Collections;

public class SpriteSheetData : MonoBehaviour {
	public int tileSize;
	public int sheetWidth;
	public int sheetHeight;
	public int palette;

	public Texture2D baseSheet;
	public Texture2D colorPalettes;
	public Texture2D coloredSheet;

	public void Initialize() {
		ResetPalettes();
	}

	public void NewSheets(int tile, int width, int height) {
		baseSheet = new Texture2D(tile*width, tile*height,TextureFormat.ARGB32,false);
		coloredSheet = new Texture2D(tile*width, tile*height,TextureFormat.ARGB32,false);
		for (int x = 0; x < 32; ++x) {
			for (int y = 0; y < 32; ++y) {
				baseSheet.SetPixel(x,y,new Color(0,0,0,0));
				coloredSheet.SetPixel(x,y,new Color(0,0,0,0));
			}
		}
	}

	public void ResetPalettes() {
		colorPalettes = new Texture2D(32,32);
		for (int x = 0; x < 32; x++) {
			for (int y = 0; y < 32; y++) {
				if(y==0) {
					colorPalettes.SetPixel(x+1,y+1,new Color((float)(x/255),(float)(x/255),(float)(x/255),1));
				} else {
					colorPalettes.SetPixel(x+1,y+1,new Color((float)((x*7)/255),(float)((x*7)/255),(float)((x*7)/255),1));
				}
			}
		}
	}
}
