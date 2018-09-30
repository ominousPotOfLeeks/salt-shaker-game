using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame1CubeTouch : MonoBehaviour {
	//miniGame1 cube movement through touching

	public float xDirection = 1;
	public float yDirection = 1;

	void Update () {
		if (Input.touchCount == 1) {
			transform.Translate (Input.touches [0].deltaPosition.x * xDirection * .05f,
								 Input.touches [0].deltaPosition.y * yDirection * .05f,
								 0);
		}
	}
}
