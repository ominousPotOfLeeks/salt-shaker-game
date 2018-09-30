using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class miniGame1Script : MonoBehaviour {
	//MiniGame1: make the cubes touch to get the cat

	public GameObject cube;
	public GameObject cat;
	public Text DebugText;
	GameObject cube1;
	GameObject cube2;
	HashSet<Collider> allCubes = new HashSet<Collider>();
	// Use this for initialization
	void Start () {
		cube1 = Instantiate(cube, new Vector3(-2f,0,0), Quaternion.identity);
		cube2 = Instantiate(cube, new Vector3(2f,0,0), Quaternion.identity);
		allCubes.Add (cube1.AddComponent<BoxCollider> ());
		allCubes.Add (cube2.AddComponent<BoxCollider> ());
	}
	
	// Update is called once per frame
	void Update () {
		//check when everything is together
		Collider[] hitColliders = Physics.OverlapSphere(new Vector3(cube1.transform.localPosition.x,
																	cube1.transform.localPosition.y,
																	0), 1);
		HashSet<Collider> cubesInCircle = new HashSet<Collider>(hitColliders);

		//if at least the cubes we made are in the circle
		if (allCubes.IsSubsetOf (cubesInCircle)) {
			//make a cat with gotCatScript (prefab)
			Instantiate (cat, new Vector3 (0, 0, 0), Quaternion.identity);
		} else {
			DebugText.text = "";
			foreach(Collider cube in cubesInCircle) {
				DebugText.text = DebugText.text + cube.gameObject.name;
			}

		}
		cube1.GetComponent<miniGame1CubeTouch>().xDirection = -1f;
	}
}
