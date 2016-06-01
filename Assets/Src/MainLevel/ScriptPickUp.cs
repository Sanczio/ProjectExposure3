using UnityEngine;
using System.Collections;

public class ScriptPickUp : MonoBehaviour {

	public enum State{
		boost,idle,attacking,eating,sleeping
	}
	public State pickUp = State.boost;
	private ScriptPlayerControls player;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "PlayerCrashCollider") {
			switch (pickUp) {
			case State.boost:
				doBoost();
				break;
			default :
				Debug.Log ("no pickup state");
				break;
			}
		}
	}


	void doBoost()
	{
		Debug.Log (" player boosted");
		player.boostPlayer(gameObject);
	}

}
