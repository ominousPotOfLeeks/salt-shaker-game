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
	float shakeDetectionThreshold = 2.0f;
	float lowPassFilterFactor;
	float wiggleThreshold = 30f;
	float wiggleBuildUp = 0f;
	float wiggleDissipation = 30f;
	float[] wiggleThresholds = new float[] {500f, 400f, 200f, 80f};
	float[] shakeThresholds = new float[] {12f, 8f, 4f, 1f};
	float[] inputTiers = new float[] {6f, 5f, 2.5f, 1f};
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
		float shake = checkShake ();
		wiggleBuildUp += checkWiggle () - wiggleDissipation;
		for (int i=0;i < inputTiers.Length;i++) {
			if (wiggleBuildUp > wiggleThresholds [i]) {
				gameObject.GetComponent<saltWrangler> ().makeThings (inputTiers[i]);
				wiggleBuildUp -= wiggleThresholds [i];
			}
			if (shake > shakeDetectionThreshold * shakeThresholds[i]) {
				gameObject.GetComponent<saltWrangler> ().makeThings (inputTiers[i]);
			}
		}
		if (checkTouch () || checkClick()) {
			// If input detected, make salt
			gameObject.GetComponent<saltWrangler> ().makeThings ();
		}
		if (wiggleBuildUp <= 0) {
			wiggleBuildUp = 0;
		}
		DebugText.text = wiggleBuildUp.ToString();
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
			if (raycastHit.collider.name == "front") {
				return true;
			} else if (raycastHit.collider.name == "p_cat(Clone)") {
				catTouched ();
			} else {
				//something unusual touched?
				DebugText.text = raycastHit.collider.name;
			}
		}
		return false;
	}

	float checkWiggle()
	{
		//wiggling finger back and forth on salt shaker
		float wiggleSize;
		if (Input.touchCount > 0 && inputCast(Input.GetTouch (0).position)) {
			wiggleSize = Mathf.Abs (Input.touches [0].deltaPosition.x);
			if (wiggleSize > wiggleThreshold) {
				return wiggleSize;
			}
		}
		return 0;
	}

	float checkShake()
	{
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;
		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold) {
			return deltaAcceleration.sqrMagnitude;
		} else {
			return 0;
		}
	}

	void catTouched()
	{
		DebugText.text = "Cat touched";
		Debug.Log ("Cat touched");
		SceneManager.LoadScene("miniGame1", LoadSceneMode.Single);
	}
}
