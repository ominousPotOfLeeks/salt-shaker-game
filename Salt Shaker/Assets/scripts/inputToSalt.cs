using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputToSalt : MonoBehaviour {
	//converts input into salt, using the power of computers

	public Text DebugText;

	//Thanks to jmpp from stackoverflow for the shake detection code
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	float shakeDetectionThreshold = 1.0f;
	float lowPassFilterFactor;
	Vector3 lowPassValue;

	void Start()
	{
		//Shake detection setup
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
		DebugText.text = "nothing";
	}

	void Update()
	{
		if (checkShake () || checkTouch ())
		{
			// If shaking detected, make salt
			gameObject.GetComponent<saltWrangler>().makeThings();
		}
	}

	bool checkTouch()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				if (raycastHit.collider.name == "front") {
					return true;
				} else if (raycastHit.collider.name == "p_cat(Clone)") {
					catTouched ();
				} else {
					DebugText.text = raycastHit.collider.name;
				}
			}
		}
		return false;
	}

	bool checkShake()
	{
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;
		return deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold;
	}

	void catTouched()
	{
		DebugText.text = "Cat touched";
		Debug.Log ("Cat touched");
		SceneManager.LoadScene("miniGame1", LoadSceneMode.Single);
	}
}
