using System;
using UnityEngine;
using System.Collections;

public class GlobalVariables
{
	public static float speed=-3f;

	public static int sceneType = 1;
	public static int sceneTypeCount = 3;
	//0 for normal, 1 for jungle, 2 for disaster

	public static int timer = 0;
	public static int previousTreeModifiedTime = 0;
	public static int updateInterval = 25;
	public static int noCollideTimer = 0;

	public static TreeGenerator treeGenerator = GameObject.Find ("TreeGenerator").GetComponent<TreeGenerator>();
	public static GameObject player = GameObject.Find ("player");
	public static ParticleSystem fog = GameObject.Find ("fog").GetComponent<ParticleSystem> ();
	public static ScreenShaker shaker = GameObject.Find ("Main Camera").GetComponent<ScreenShaker> ();

	public void setSpeed(float n) {
		speed = n;
	}

	public void setScene(int s) {
		sceneType = s;
	}
}

