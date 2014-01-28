using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class SpriteController : MonoBehaviour {
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}

	public void SetAnim(string id) {
		anim.Play(id);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			SetAnim("KnightBlock");
		}
	}
}
