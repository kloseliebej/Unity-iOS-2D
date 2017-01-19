using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float maxSpeed = 10f;
	Animator anim;
	public Rigidbody2D Char;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	float jumpForce = 200.0f;
		
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Char = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float move = Input.GetAxis ("Horizontal");
		//Debug.Log (grounded);
		Char.velocity = new Vector2 (move * maxSpeed, Char.velocity.y);


	
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.S)){
			anim.SetTrigger ("Crunch");
		}

		if (grounded && Input.GetKeyDown (KeyCode.W)) {
			//Debug.Log ("w");
//
//			Debug.Log (Char.velocity.y);
			grounded = false;
			anim.SetBool ("Ground", false);
			Char.AddForce (transform.up * jumpForce);

		}
	}
}
