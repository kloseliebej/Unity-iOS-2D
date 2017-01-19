using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float maxSpeed = 10f;
	Animator anim;
	public Rigidbody2D Char;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	float jumpForce = 8f;

	int blinkTimer = 0;

	//cooldowns
	int lastE = 0;
	int cooldownE = 0;
	int lastQ = 0;
	int cooldownQ = 0;

	//public static Vector2 up;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Char = GetComponent<Rigidbody2D>();
		cooldownE = 5 * (int)(1.0f / Time.deltaTime);
		cooldownQ = 5 * (int)(1.0f / Time.deltaTime);
		lastE = -cooldownE;
		lastQ = -cooldownQ;
	}

	// Update is called once per frame
	void FixedUpdate () {
		

	}

	void OnCollisionEnter2D (Collision2D collision) {
		//Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name == "tree1(Clone)" || collision.gameObject.name == "tree2(Clone)") {
			Debug.Log ("collision_tree");
			Application.LoadLevel ("gameover");
		}
	}

	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		GlobalVariables.timer++;
		if (GlobalVariables.noCollideTimer > 0) {
			blinkTimer--;
			if (blinkTimer == 0) {
				blinkTimer = (int)(1.0f / Time.deltaTime) / 8;
				Color c = GetComponent<SpriteRenderer> ().color;
				c.a = 0.5f + (1.0f - c.a );
				GetComponent<SpriteRenderer> ().color = c;
			}

			GlobalVariables.noCollideTimer--;
			if (GlobalVariables.noCollideTimer == 0) {
				foreach (GameObject tree in GlobalVariables.treeGenerator.trees) {
					tree.GetComponent<PolygonCollider2D> ().enabled = true;
				}
				blinkTimer = 0;
				Color c = GetComponent<SpriteRenderer> ().color;
				c.a = 1.0f;
				GetComponent<SpriteRenderer> ().color = c;
				lastE = GlobalVariables.timer;
			}
		}

		if (Input.GetKey ("s")) {
			anim.SetTrigger ("Crunch");
		} else if (/*grounded && */Input.GetKey ("w") && Char.velocity.y==0) {
			grounded = false;
			anim.SetBool ("Ground", false);
			//Char.AddForce (transform.up * jumpForce);
			Char.AddForce (jumpForce * Vector2.up, ForceMode2D.Impulse);
			Debug.Log ("jump force : ");
			Debug.Log (transform.up * jumpForce);
		} 
//			else if (Input.GetKey ("d") && Mathf.Abs (Char.velocity.x) < Mathf.Max (-GlobalVariables.speed + 1, 5)) {
//			Char.AddForce (1 * Vector2.right, ForceMode2D.Impulse);
//		} else if (Input.GetKey ("a") && Mathf.Abs (Char.velocity.x) < Mathf.Max (-GlobalVariables.speed + 1, 5)) {
//			Char.AddForce (1 * Vector2.left, ForceMode2D.Impulse);
//		} 
		else if (Input.GetKey ("q")) {
			if (GlobalVariables.timer - lastQ < cooldownQ)
				return;
			GameObject tree = null;
			float minPos = 100;
			foreach (GameObject t in GlobalVariables.treeGenerator.trees) {
				if (t.transform.position.x - transform.position.x < 0 || t.transform.position.x - transform.position.x > minPos)
					continue;
				tree = t;
				minPos = t.transform.position.x - transform.position.x;
			}
			if (minPos == 100)
				return;
			tree.GetComponent<PolygonCollider2D> ().enabled = false;
			Debug.Log ("target:" + tree.transform.position);
			GlobalVariables.treeGenerator.removeTree (tree);
			lastQ = GlobalVariables.timer;
		} else if (Input.GetKey ("e")) {
			if (GlobalVariables.timer - lastE < cooldownE)
				return;
			GlobalVariables.noCollideTimer = 5 * (int)(1.0f / Time.deltaTime);
			foreach (GameObject tree in GlobalVariables.treeGenerator.trees) {
				tree.GetComponent<PolygonCollider2D> ().enabled = false;
			}
			blinkTimer = 4 * (int)(1.0f / Time.deltaTime);
			Color c = GetComponent<SpriteRenderer> ().color;
			c.a = 0.5f;
			GetComponent<SpriteRenderer> ().color = c;
		} else if (Input.GetKey ("z")) {
			ParticleSystem.EmissionModule module = GlobalVariables.fog.emission;
			module.enabled = false;
			GlobalVariables.shaker.enabled = false;
		}
	}
}
