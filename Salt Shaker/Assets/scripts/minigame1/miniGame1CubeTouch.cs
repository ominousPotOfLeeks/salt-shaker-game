using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame1CubeTouch : MonoBehaviour {
	//miniGame1 cube movement through touching

	public float xDirection = 1;
	public float yDirection = 1;
	
	Vector3 delta = Vector3.zero;
	Vector3 lastPos = Vector3.zero;


	void Update () {
		if (Input.touchCount == 1) {
			transform.Translate (Input.touches [0].deltaPosition.x * xDirection * .05f,
								 Input.touches [0].deltaPosition.y * yDirection * .05f,
								 0);
		}
		//Thanks to Alucardj from unity answers for this method of checking mouse delta
		if (Input.GetMouseButtonDown (0)) {
			lastPos = Input.mousePosition;
		} else if (Input.GetMouseButton (0)) {
			delta = Input.mousePosition - lastPos;
			transform.Translate (delta.x * xDirection * .05f,
				delta.y * yDirection * .05f,
				0);
			lastPos = Input.mousePosition;
		}
	}
}
