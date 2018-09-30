using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class saltWrangler : MonoBehaviour {
	//deals with the details of salt production

	public GameObject Cat;
	public Text DebugText;
	public float catRarity;//1 in catRarity chance of salt being a cat.

	public float saltScale;//size of salt
	public float saltScaleVariation;//variation in salt size
	public int saltDensity;//amount of salt produced at once

	public void makeThings ()
	{
		for (int i = 0; i < saltDensity; i++) {
			if (Mathf.Floor(Random.Range(0,catRarity)) == 0) {
				//spawn cat
				makeCat (gameObject.transform.position.x, gameObject.transform.position.y, saltScale, 10f);
			} else {
				//just salt
				makeSalt (gameObject.transform.position.x, gameObject.transform.position.y, saltScale, 10f);
			}
		}
	}

	public void makeCat(float x, float y, float scale, float speed)
	{
		GameObject icat = Instantiate(Cat, new Vector3(x+0.02f,y,0), Quaternion.identity);
		Rigidbody rb = icat.AddComponent<Rigidbody>();
		rb.isKinematic = true;
		var boxCollider = icat.AddComponent<BoxCollider> ();
		boxCollider.isTrigger = true;
		icat.transform.localScale = new Vector3 (28f, 28f, 28f);
		icat.transform.localRotation = Quaternion.Euler (0, 180, 0);
	}

	public void makeSalt(float x, float y, float scale, float speed)
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
	}
}
