using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeGenerator : MonoBehaviour {

	int treeType = 2;
	public GameObject[] treeTypes;

	public List<GameObject> trees = new List<GameObject> ();
	int interval = 0;
	int minInterval = 1;
	int unitTime = 0;

	// Use this for initialization
	void Start () {
		treeTypes = new GameObject[treeType];
		unitTime = (int)(1.0f / Time.deltaTime) / 4;
		minInterval = 6 * unitTime;
		for (int i = 0; i < treeType; i++)
			treeTypes [i] = GameObject.Find ("tree" + (i+1));
	}

	// Update is called once per frame
	void Update () {
		if (trees.Count>0 && trees [0].transform.position.x < -9) {
			GameObject t = trees [0];
			trees.RemoveAt (0);
			Destroy (t);
		}
		interval++;
		if (interval < minInterval || (interval - minInterval) % unitTime > 0)
			return;
		if (Random.Range (0, 10) > (interval-minInterval) / unitTime)
			return;
		interval = 0;
		int type = Random.Range (0, treeType);
		GameObject newTree = (GameObject)Instantiate (treeTypes [type], new Vector3 (8f, -2.79f, 70f), Quaternion.identity);
		if (GlobalVariables.sceneType == 1 && Random.Range (0, 8) == 0) {
			Vector3 scale = newTree.transform.localScale;
			scale.x *= 2.5f;
			scale.y *= 2.5f;
			newTree.transform.position = new Vector3 (8f, -1.78f, 70f);
			newTree.transform.localScale = scale;
		}
		if (GlobalVariables.noCollideTimer > 0)
			newTree.GetComponent<PolygonCollider2D> ().enabled = false;
		trees.Add (newTree);
	}

	public void removeTree(GameObject tree) {
		for (int i = 0; i < trees.Count; i++) {
			GameObject obj = trees [i];
			if (Object.ReferenceEquals (obj, tree)) {
				Debug.Log (tree.transform
					.position + " " + obj.transform.position);
				trees.RemoveAt (i);
				tree.GetComponent<tree> ().playAnimation ();
				Destroy (tree, 1);
				break;
			}
		}
	}
}