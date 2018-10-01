using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class miniGame1MeetingPoint : MonoBehaviour {
	//MiniGame1: make the cubes mostly overlap to get the cat

	public GameObject b_cube;
	public GameObject w_cube;
	public GameObject cat;
	public Text DebugText;
	
	GameObject[] allCubes = new GameObject[2];

	bool madeCat = false;// has the puzzle been solved

	void Start () {
		allCubes[0] = Instantiate(b_cube, new Vector3(-2f,0,0), Quaternion.identity);
		allCubes[1] = Instantiate(w_cube, new Vector3(2f,0,0), Quaternion.identity);
	}

	void Update () {
		
		if (madeCat) {
			//slow down movement on puzzle completion for dramatic effect
			setCubePolarity(0, new Vector2(-0.1f, 0.1f));
			setCubePolarity(1, new Vector2(0.1f, 0.1f));
		} else {
			//check when everything is together
			if (inCircle (allCubes [0].transform.position, allCubes [1].transform.position, 0.1f)) {
				//make a cat that has gotCatScript (prefab)
				Instantiate (cat, new Vector3 (0, 0, 0), Quaternion.identity);
				madeCat = true;
			}
			//set directions
			setCubePolarity(0, new Vector2(-1f, 1f));
		}
	}

	void setCubePolarity(int cubeNum, Vector2 polarity) {
		//sets strength and direction of a cube's response to input
		allCubes[cubeNum].GetComponent<miniGame1CubeTouch>().polarity = polarity;
	}

	bool inCircle(Vector3 position, Vector3 center, float radius) {
		//returns whether a point at position is within the circle
		float xSqrd = Mathf.Pow (position.x - center.x, 2);//  a.k.a position.x * position.x
		float ySqrd = Mathf.Pow (position.y - center.y, 2);//  a.k.a position.x * position.x
		return xSqrd + ySqrd < radius;
	}
}
