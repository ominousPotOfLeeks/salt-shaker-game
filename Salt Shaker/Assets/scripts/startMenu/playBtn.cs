using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playBtn : MonoBehaviour {

	void Update () {
		if (checkTouch () || checkClick ()) {
			SceneManager.LoadScene ("mainGame", LoadSceneMode.Single);
		}
	}

	bool checkTouch()
	{
		//checks for touch to saltshaker or cat.
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			if (inputCast(Input.GetTouch (0).position)) {
				return true;
			}
		}
		return false;
	}

	bool checkClick()
	{
		if (Input.GetMouseButtonDown (0)) {
			if (inputCast (Input.mousePosition)) {
				return true;
			}
		}
		return false;
	}

	bool inputCast(Vector3 position)
	{
		Ray raycast = Camera.main.ScreenPointToRay(position);
		RaycastHit raycastHit;
		if (Physics.Raycast(raycast, out raycastHit))
		{
			if (raycastHit.collider.name == "play_box") {
				return true;
			}
		}
		return false;
	}
}
