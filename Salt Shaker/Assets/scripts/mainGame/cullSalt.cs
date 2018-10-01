using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullSalt : MonoBehaviour {
	//Destroys any trigger object that touches it (usually salt, sometimes cats etc.)

	void OnTriggerEnter(Collider col) {
		Destroy (col.gameObject);
		//Debug.Log("Destroyed" + col.gameObject.name);
	}
}
