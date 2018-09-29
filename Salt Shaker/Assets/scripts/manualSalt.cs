using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manualSalt : MonoBehaviour {

	public GameObject Cat;
	public Text DebugText;
	public float catRarity;

	public float saltScale;
	public float saltScaleVariation;
	public int saltDensity;

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
			for (int i = 0; i < saltDensity; i++) {
				makeThings();
			}
		}
	}

	bool checkTouch()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch (0).position);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				makeCat (gameObject.transform.position.x + 2f, gameObject.transform.position.y, saltScale, 10f);
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

	GameObject makeThings ()
	{
		GameObject thing;
		if (Mathf.Floor(Random.Range(0,catRarity)) == 0) {
			//spawn cat
			thing = makeCat (gameObject.transform.position.x, gameObject.transform.position.y, saltScale, 10f);
		} else {
			//just salt
			thing = makeSalt (gameObject.transform.position.x, gameObject.transform.position.y, saltScale, 10f);
		}
		return thing;
	}

	GameObject makeCat(float x, float y, float scale, float speed)
	{
		GameObject icat = Instantiate(Cat, new Vector3(x+0.02f,y,0), Quaternion.identity);
		Rigidbody rb = icat.AddComponent<Rigidbody>();
		rb.isKinematic = true;
		var boxCollider = icat.AddComponent<BoxCollider> ();
		boxCollider.isTrigger = true;
		icat.transform.localScale = new Vector3 (28f, 28f, 28f);
		icat.transform.localRotation = Quaternion.Euler (0, 180, 0);
		return icat;
	}

	GameObject makeSalt(float x, float y, float scale, float speed)
	{
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.AddComponent<Rigidbody>();
		var boxCollider = cube.AddComponent<BoxCollider> ();
		boxCollider.isTrigger = true;
		float seed = Random.Range (-1, 1);
		float seed2 = (Random.Range (0, 1) * 5f % 2f) - 1;
		float seed3 = Random.Range (-1, 1);
		float seed4 = Random.Range (-1, 1);
		float seed5 = Random.Range (-1, 1);
		float seed6 = Random.Range (-1, 1);
		float seed7 = Random.Range (-1, 1);
		cube.transform.position = new Vector3(x + seed/8f, y + seed2/8f, 0); 
		cube.transform.localScale = new Vector3(scale * (1+saltScaleVariation*seed3), 
												scale * (1+saltScaleVariation*seed4), 
												scale * (1+saltScaleVariation*seed5));
		cube.transform.localRotation = Quaternion.Euler (seed5*180, seed6*180, seed7*180);
		return cube;
	} 
}
