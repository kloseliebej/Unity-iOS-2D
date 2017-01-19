using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (GlobalVariables.speed, 0);
		if (transform.position.x <= -16.89) {
			Vector3 oldPos = transform.position;
			transform.position = new Vector3 (17.11f, oldPos.y, oldPos.z);
		}
	}
}
