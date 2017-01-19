using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	int updateTimer = 0;
	int disasterTimer = 0;
	int lastScene = 0;

	// Use this for initialization
	void Start ()
	{
		updateTimer = 3 * (int)(1.0f / Time.deltaTime);
	}

	// Update is called once per frame
	void Update ()
	{
		updateTimer--;
		if (updateTimer == 0) {
			int type = Random.Range (0, GlobalVariables.sceneTypeCount);
			while (type == lastScene)
				type = Random.Range (0, GlobalVariables.sceneTypeCount);
			lastScene = type;
			GlobalVariables.sceneType = type;
			updateTimer = 10 * (int)(1.0f / Time.deltaTime);
			ParticleSystem.EmissionModule module = GlobalVariables.fog.emission;
			module.enabled = false;
			GlobalVariables.shaker.enabled = false;
			Debug.Log (lastScene);
		}
		if (disasterTimer > 0)
			disasterTimer--;
		if (lastScene == 2 && disasterTimer == 0) {
			ParticleSystem.EmissionModule module = GlobalVariables.fog.emission;
			module.enabled = false;
			GlobalVariables.shaker.enabled = false;

			int disaster = Random.Range (0, 2);
			Debug.Log ("disaster "+disaster);
			if (disaster == 0) {    //fog
				module = GlobalVariables.fog.emission;
				module.enabled = true;
				GlobalVariables.fog.Play ();
			} else if (disaster == 1) {
				GlobalVariables.shaker.enabled = true;
			}
			disasterTimer = 10 * (int)(1.0f / Time.deltaTime);
		}
	}
}