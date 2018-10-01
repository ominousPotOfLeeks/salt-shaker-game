using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class arrow : MonoBehaviour {

	public Text DebugText;
	public string shopName = "Shop";

	public float animationSpeed = 3.0f;
	public float animationSize = 1.0f;
	float initialx;
	public int direction = 1;//1 for point/move right, -1 for point/move left

	void Start () {
		transform.localRotation = Quaternion.Euler (0, 90f + direction * 90f, 0);
		if (DebugText == null) {
			DebugText = GameObject.Find (shopName).GetComponent<shopController>().DebugText;
		}

		initialx = gameObject.transform.localPosition.x;
		//DebugText.text += initialx.ToString();
		gameObject.transform.Translate(new Vector3(direction * animationSize, 0, 0));
	}

	void Update () {
		DebugText.text = initialx.ToString() + ", " + gameObject.transform.localPosition.x.ToString();

		//move arrow
		if ((direction * gameObject.transform.localPosition.x < direction * initialx)) {
			gameObject.transform.Translate (new Vector3 (direction * getSpeed (), 0, 0));
		} else {
			gameObject.transform.localPosition = new Vector3 ((initialx - direction * animationSize),
														 gameObject.transform.localPosition.y,
														 gameObject.transform.localPosition.z);
		}
	}

	float getSpeed() {
		//calculates what the speed of the arrow should be

		//go forward, getting slower as it gets closer
		float baseSpeed = direction * -animationSpeed * Time.deltaTime;
		return baseSpeed * (Mathf.Abs(initialx - gameObject.transform.position.x) + 1f);
	}
}
