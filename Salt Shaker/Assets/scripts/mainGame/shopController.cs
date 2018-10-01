using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class shopController : MonoBehaviour {
	//tracks salt consumed (score), deals with shop

	public static bool shopOpen = false;//set to true when first non-salt (probably a cat) item is collected

	public Text DebugText;
	public Camera mainCamera;
	public GameObject arrow;

	public float transitionSpeed = 15f;
	float screenEdge;

	bool shopBrowsing = false; // user is looking at shop
	bool inTransition = false; // moving camera
	bool arrowDrawn = false;

	void Start () {
		screenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
		//basically redundant as the world position of the screen edge shouldn't change
	}

	void Update () {
		if (shopOpen) {
			if (shopBrowsing) {
				
			} else {
				showShopIndicator ();
				if (shopTouched () || inTransition) {
					hideShopIndicator ();
					inTransition = true;
					openShop ();
				}
			}
		}
	}

	bool shopTouched () {
		//check if swipe from right
		if (Input.touchCount > 0 && inputCast(Input.GetTouch (0).position)) {
			DebugText.text = "shopHandle touched" + Input.touches [0].deltaPosition.x.ToString();
			if (Input.touches [0].deltaPosition.x < 0) {
				return true;
			}
		}
		return false;
	}

	void showShopIndicator () {
		//some kind of blinking arrow or indicator
		DebugText.text = "shop open";
		DebugText.text += screenEdge.ToString ();
		if (!arrowDrawn) {
			Instantiate (arrow, new Vector3 (screenEdge-1.5f, 0, 0), Quaternion.identity);
			DebugText.text += screenEdge.ToString ();
			arrowDrawn = true;
		}
	}
	void hideShopIndicator () {
		//hide the blinking arrow
		DebugText.text = "shop opening";
	}

	void openShop () {
		//slide camera over to shop
		DebugText.text = "moving camera" + mainCamera.transform.position.x.ToString() + gameObject.transform.position.x.ToString();
		if (mainCamera.transform.position.x < gameObject.transform.position.x) {
			mainCamera.transform.Translate (new Vector3 (transitionSpeed * Time.deltaTime, 0, 0));
		} else {
			inTransition = false;
			shopBrowsing = true;
			mainCamera.transform.position = new Vector3 (gameObject.transform.position.x, 
														 mainCamera.transform.position.y, 
														 mainCamera.transform.position.z);
		}
	}

	bool inputCast(Vector3 position)
	{
		Ray raycast = Camera.main.ScreenPointToRay(position);
		RaycastHit raycastHit;
		if (Physics.Raycast(raycast, out raycastHit))
		{
			if (raycastHit.collider.name == "shopHandle") { //like a door handle to the shop
				return true;
			}
		}
		return false;
	}
}
