using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullSalt : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		Destroy (col.gameObject);
		//Destroy (gameObject);
		Debug.Log("Destroyed" + col.gameObject.name);
	}
}
