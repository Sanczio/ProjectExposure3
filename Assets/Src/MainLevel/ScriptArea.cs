using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptArea : MonoBehaviour {

	List<GameObject> bio_spawns = new List<GameObject>();
	List<GameObject> recy_spawns = new List<GameObject>();

	private bool grayscaledArea = true;
	private ScriptCameraControl cameraScript;


	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "recycable_spawn") {
			recy_spawns.Add (collider.gameObject);

		}
		if (collider.tag == "bio_spawn") {
			bio_spawns.Add (collider.gameObject);
		}



	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "PlayerCrashCollider") {
			if (grayscaledArea)
				cameraScript.setGrayscale (0.7f);
			else
				cameraScript.setGrayscale (0.1f);	
		}
	}



	IEnumerator BlockRoads( float time)
	{
		
		yield return new WaitForSeconds (time);
		if ( gameObject.name != "Tutorial")
			gameObject.GetComponent<Collider> ().isTrigger = false;
	}

	void Start()
	{
		cameraScript = GameObject.Find ("Main Camera").GetComponent<ScriptCameraControl> ();
		StartCoroutine (BlockRoads (2));
	}



	public List<GameObject> getBioSpawns()
	{
		return bio_spawns;
	}

	public List<GameObject> getRecySpawns()
	{
		return recy_spawns;
	}

	public void setGrayscaledArea(bool toGrayscale)
	{
		grayscaledArea = toGrayscale;

		for (int i = 0; i < gameObject.transform.GetChildCount (); i++) {
			gameObject.transform.GetChild (i).GetComponent<Renderer> ().enabled = false;
		}


	}


}
