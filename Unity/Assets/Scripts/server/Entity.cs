using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class Entity : MonoBehaviour {

	private SpriteRenderer sprite;

	public Vector3 startPos;
	public Vector3 targetPos;

	public void Flip() {
		if(transform.localScale.x > 0) 
			transform.localScale = new Vector3(-1,1,1);
		else
			transform.localScale = new Vector3(1,1,1);
	}


	void Awake() {
		sprite = GetComponent<SpriteRenderer>();

		startPos = transform.position;
	}

	public void Activate() {
		sprite.sortingOrder = 1;
	}

	public void DeActivate() {
		sprite.sortingOrder = 0;
	}

	// Update is called once per frame
	void Update () {
	}
}
