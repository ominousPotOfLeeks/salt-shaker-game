using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotCatScript : MonoBehaviour {
	//You unlocked the ability to buy cats, so return to the main game

	public GameObject shaker;
	public float animationSpeed = 5f;
	public float finalScale = 800f;

	float initialScale;

	void Start () {
		transform.localRotation = Quaternion.Euler (0, 180, 0);
		initialScale = transform.localScale.x;
	}

	void Update () {
		if (transform.localScale.x > initialScale*finalScale) {
			//once big enough, go somewhere else
			SceneManager.LoadScene ("mainGame", LoadSceneMode.Single);
			shopController.shopOpen = true;
		} else {
			//spin and expand 
			transform.Rotate (Vector3.forward * Time.deltaTime * 400f, Space.World);
			transform.localScale += new Vector3 (initialScale, initialScale, initialScale) * Time.deltaTime * animationSpeed;
			animationSpeed *= 1.02f;
		}
	}
}
