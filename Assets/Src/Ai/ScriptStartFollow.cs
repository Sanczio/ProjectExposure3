using UnityEngine;
using System.Collections;

public class ScriptStartFollow : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerStay(Collider collidingObject)
	{
		if (collidingObject.tag == "EnemyCar")
		{
			FollowPlayer(collidingObject);

		}
	}

	void FollowPlayer(Collider playerCollider)
	{
		ScriptCivilCar tempScript = playerCollider.GetComponent<ScriptCivilCar>();
		tempScript.setFollowing(true);
		tempScript.FollowPlayer(this.transform.position);
	}

	void OnTriggerExit(Collider collidingObject)
	{
		if (collidingObject.tag == "EnemyCar")
		{
			GoBackToRoad(collidingObject);
		}
	}

	void GoBackToRoad(Collider collidingObject)
	{
		ScriptCivilCar tempScript = collidingObject.GetComponent<ScriptCivilCar>();
		tempScript.setFollowing(false);
		tempScript.GetBackToWaypoint();
	}

}
