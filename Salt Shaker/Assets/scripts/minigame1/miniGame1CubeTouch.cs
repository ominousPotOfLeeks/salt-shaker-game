using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame1CubeTouch : MonoBehaviour {
	//miniGame1 cube movement through touching

	public Vector2 polarity = new Vector2(1f,1f);

	public float touchScale = 0.02f;
	public float mouseScale = 0.01f;
	
	Vector3 delta = Vector3.zero;
	Vector3 lastPos = Vector3.zero;


	void Update () {
		if (Input.touchCount == 1) {
			transform.Translate (Input.touches [0].deltaPosition.x * polarity.x * touchScale,
								 Input.touches [0].deltaPosition.y * polarity.y * touchScale,
								 0);
		}
		//Thanks to Alucardj from unity answers for this method of checking mouse delta
		if (Input.GetMouseButtonDown (0)) {
			lastPos = Input.mousePosition;
		} else if (Input.GetMouseButton (0)) {
			delta = Input.mousePosition - lastPos;
			transform.Translate (delta.x * polarity.x * mouseScale,
								 delta.y * polarity.y * mouseScale,
								 0);
			lastPos = Input.mousePosition;
		}
	}
}
