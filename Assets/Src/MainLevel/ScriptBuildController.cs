using UnityEngine;
using System.Collections;

public class ScriptBuildController : MonoBehaviour {

	private GameObject solarpanel;
	private string solarpanel_name;

	private GameObject windmill;
	private string windmill_name;

	void Start()
	{
		ScriptSettingsGameplay gameplaySettings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();

		solarpanel_name = gameplaySettings.prefab_n_solarpanel;
		windmill_name = gameplaySettings.prefab_n_windmill;

		solarpanel = (GameObject)Resources.Load("prefabs/"+solarpanel_name);
		windmill = (GameObject)Resources.Load("prefabs/"+windmill_name);
	}

	public bool AskToPlaceSolarPanel(RaycastHit hit)
	{
		if (hit.collider.gameObject.tag == "house") {
			//Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			//spawnPosition.Set (spawnPosition.x, spawnPosition.y, hit.collider.gameObject.transform.position.z);
			GameObject tempGameObj = hit.collider.gameObject;
			Vector3 spawnPosition = tempGameObj.transform.position;
			spawnPosition.Set (spawnPosition.x, spawnPosition.y + 3.0f, spawnPosition.z);
			Instantiate (solarpanel, spawnPosition , tempGameObj.transform.rotation);
			return true;
		}
		return false;
	}

	public bool AskToPlaceWindmill(RaycastHit hit)
	{
		if (hit.collider.gameObject.name == "windmillzone") {
			
			//Vector3 spawnPositionMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0,Input.mousePosition.y));
			//spawnPositionMouse.Set (spawnPositionMouse.x,  hit.collider.gameObject.transform.position.y ,spawnPositionMouse.z);
			//myGameObject.transform.position = Vector3.Lerp(myGameObject.transform.position, a, 0.01f);

			GameObject tempGameObj = hit.collider.gameObject;
			Vector3 spawnPosition = tempGameObj.transform.position;
			spawnPosition.Set (hit.point.x, hit.point.y, hit.point.z);
			Instantiate (windmill, spawnPosition , tempGameObj.transform.rotation);
			return true;
		}
		return false;
	}
}
