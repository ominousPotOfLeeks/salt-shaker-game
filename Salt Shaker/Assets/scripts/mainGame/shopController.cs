using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class shopController : MonoBehaviour {
	//tracks salt consumed (score), deals with shop

	public static bool shopOpen = true;//set to true when first non-salt (probably a cat) item is collected
	public static bool gotCat = true;

	public Text DebugText;
	public Camera mainCamera;
	public GameObject arrow;
	public GameObject cat;

	public string SHOPHANDLENAME = "shopHandle";
	public float SWIPE_THRESHOLD = 0f;
	public float TRANSITION_SPEED = 15f;
	float screenEdge;
	float initialCameraX;

	bool shopBrowsing = false; // user is looking at shop
	bool inTransition = false; // moving camera
	bool arrowDrawn = false;
	bool catDrawn = false;

	void Start () {
		screenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
		initialCameraX = mainCamera.transform.position.x;
		//basically redundant as the world position of the screen edge shouldn't change
	}

	void Update () {
		if (shopOpen) {
			if (shopBrowsing) {

				if (shopSwipeClosed () || inTransition) {
					closeShop ();
				}
			} else {
				showShopIndicator ();
				if (shopSwipeOpen () || inTransition) {
					hideShopIndicator ();
					openShop ();
				}
			}
		}
		if (gotCat && !catDrawn) {
			catDrawn = true;
			Instantiate (cat, new Vector3 (gameObject.transform.position.x - 1.5f, 1.5f, 0), Quaternion.Euler (0, 180, 0));
		}
	}

	bool shopSwipeOpen () {
		//check if swipe from right
		if (Input.touchCount > 0 && inputCast(Input.GetTouch (0).position, SHOPHANDLENAME)) {
			DebugText.text = "shopHandle touched" + Input.touches [0].deltaPosition.x.ToString();
			if (Input.touches [0].deltaPosition.x < 0) {
				return true;
			}
		}
		return false;
	}

	bool shopSwipeClosed () {
		//check if swipe from right
		if (Input.touchCount > 0 && inputCast(Input.GetTouch (0).position, SHOPHANDLENAME)) {
			DebugText.text = "shopHandle touched" + Input.touches [0].deltaPosition.x.ToString();
			if (Input.touches [0].deltaPosition.x > 0) {
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
		if (!inTransition) {
			DebugText.text = "moving camera from " + initialCameraX + " to " + gameObject.transform.position.x.ToString();
		}
		if (mainCamera.transform.position.x < gameObject.transform.position.x) {
			inTransition = true;
			mainCamera.transform.Translate (new Vector3 (TRANSITION_SPEED * Time.deltaTime, 0, 0));
		} else {
			inTransition = false;
			shopBrowsing = true;
			mainCamera.transform.position = new Vector3 (gameObject.transform.position.x, 
														 mainCamera.transform.position.y, 
														 mainCamera.transform.position.z);
		}
	}

	void closeShop () {
		//slide camera over to shop
		if (!inTransition) {
			DebugText.text = "moving camera back from " + mainCamera.transform.position.x.ToString() + " to " +  initialCameraX.ToString();
		}
		if (mainCamera.transform.position.x > initialCameraX) {
			inTransition = true;
			mainCamera.transform.Translate (new Vector3 (-TRANSITION_SPEED * Time.deltaTime, 0, 0));
		} else {
			inTransition = false;
			shopBrowsing = false;
			//just confirm it is in the correct position, and not slightly past it
			mainCamera.transform.position = new Vector3 (initialCameraX, 
														 mainCamera.transform.position.y, 
														 mainCamera.transform.position.z);
		}
	}

	bool inputCast(Vector3 position, string name)
	{
		Ray raycast = Camera.main.ScreenPointToRay(position);
		RaycastHit raycastHit;
		if (Physics.Raycast(raycast, out raycastHit))
		{
			if (raycastHit.collider.name == name) { //like a door handle to the shop
				return true;
			}
		}
		return false;
	}
}
