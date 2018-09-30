using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCatScript : MonoBehaviour {
	//Kinematic cat goes down at a constant SPEED

	public float SPEED;

	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * SPEED);
	}
}
