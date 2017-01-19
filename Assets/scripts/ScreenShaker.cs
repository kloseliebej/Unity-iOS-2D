using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	
	Camera c;
	public float shakeAmount;
	// Use this for initialization
	void Start () {
		c = GetComponent<Camera>();
		shakeAmount = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
		c.transform.localPosition = Random.insideUnitCircle * shakeAmount;
	
	}
}
