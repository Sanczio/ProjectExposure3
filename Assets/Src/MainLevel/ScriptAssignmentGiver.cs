using UnityEngine;
using System.Collections;

public class ScriptAssignmentGiver : MonoBehaviour {

	public enum State{
		tutorial,area1,area2,area3
	} // 0 - tuto , 1 - area1 , etc.

	private	ScriptAssignmentController assignmentController;
	public State giver = State.tutorial;


	void Start()
	{
		assignmentController = GameObject.Find ("Root").GetComponent<ScriptAssignmentController> ();
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "PlayerCrashCollider") {
			int giverNr = 0;
			switch (giver) {
			case State.tutorial: giverNr = 0; break;
			case State.area1: giverNr = 1; break;
			case State.area2: giverNr = 2; break;
			case State.area3: giverNr = 3; break;
			}
			assignmentController.setDeliveringGoods (giverNr);
		}
	}
}
