using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCatScript : MonoBehaviour {

	public float SPEED;

	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * SPEED);
	}
}
