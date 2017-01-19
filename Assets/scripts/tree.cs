using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {
	Rigidbody2D rigid;
	public int type;
	Animator anim;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = new Vector2 (GlobalVariables.speed, 0);	//change to position
	}

	public void playAnimation() {
		if (type == 1)
			anim.Play ("cutdown1");
		else
			anim.Play ("cutdown2");
	}
}
