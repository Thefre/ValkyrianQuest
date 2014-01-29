using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
	private SpriteRenderer sprite;
	private SpriteController spriteController;
	public Vector3 startPos;
	public Vector3 targetPos;

	// Use this for initialization
	void Start () {
		Transform findSprite = transform.FindChild("Sprite");
		sprite = findSprite.GetComponent<SpriteRenderer>();
		spriteController = findSprite.GetComponent<SpriteController>();
		startPos = transform.position;
	}
	
	public void Flip() {
		if(transform.localScale.x > 0)
			transform.localScale = new Vector3(-1,1,1);
		else
			transform.localScale = new Vector3(1,1,1);
	}

	public void Activate() {
		sprite.sortingOrder = 1;
	}
	
	public void DeActivate() {
		sprite.sortingOrder = 0;
	}
}
