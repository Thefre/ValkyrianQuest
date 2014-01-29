using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Flip ();
	}
	
	public void Flip() {
		if(transform.localScale.x > 0)
			transform.localScale = new Vector3(-1,1,1);
		else
			transform.localScale = new Vector3(1,1,1);
	}
}
