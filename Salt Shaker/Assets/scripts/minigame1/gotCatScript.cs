using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotCatScript : MonoBehaviour {
	
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		if (transform.localScale.x > 15f) {
			SceneManager.LoadScene ("mainGame", LoadSceneMode.Single);
		} else {
			transform.Rotate (Vector3.up * Time.deltaTime, Space.World);
			transform.localScale += new Vector3 (1, 1, 1) * Time.deltaTime;
		}
	}
}
