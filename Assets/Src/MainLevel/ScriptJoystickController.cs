using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptJoystickController : MonoBehaviour {

	private Button controller;

	void Start() 
	{
		controller = gameObject.GetComponent<Button> ();
	}



}
