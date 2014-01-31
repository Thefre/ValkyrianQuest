using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class SpriteController : MonoBehaviour {
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();

		Window newWindow = new Window();
		newWindow.Initialize("dev",5,5,150,200,AnimTest,Window.Align.TopLeft,"Animation Test");
		GUIManager.windows.Add(newWindow);
	}

	public void SetAnim(string id) {
		anim.Play(id);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			GUIManager.state = "dev";
		}
	}

	public void AnimTest() {
		if (GUILayout.Button("Idle"))
			SetAnim("KnightIdle");
		if (GUILayout.Button("Attack"))
			SetAnim("KnightAttack");
		if (GUILayout.Button("Hurt"))
			SetAnim("KnightHurt");
		if (GUILayout.Button("Block"))
			SetAnim("KnightBlock");
		if (GUILayout.Button("Death"))
			SetAnim("KnightDeath");

	}
}
