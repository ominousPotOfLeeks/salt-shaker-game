using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotCatScript : MonoBehaviour {
	//You unlocked the ability to buy cats, so return to the main game
	
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		if (transform.localScale.x > 15f) {
			//once big enough, go somewhere else
			SceneManager.LoadScene ("mainGame", LoadSceneMode.Single);
		} else {
			//spin and expand 
			transform.Rotate (Vector3.up * Time.deltaTime, Space.World);
			transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f) * Time.deltaTime;
		}
	}
}
