using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor;
	public bool steering;
}


public class ScriptPlayerControls : MonoBehaviour {

	private float tempCameraDistance;
	private float closeCameraDistance = 4.0f;
	private float deltaTime = 0.0f;
	private float trashTime = 7.0f;
	private int trashCollectedBio = 0;
	private int[] trashCollectedRecy = {0,0,0}; // 0 - a , 1 - b , 2 - c
	private int numOfBuildingCanBuild = 0;
	private string labelText = "";
	private GameObject tempTrash;
	private ScriptSettingsControls controlSettings;
	private ScriptBuildController buildControllerScript;
	private ScriptPlayerHUD hud;

	private Rigidbody playerRigidbody;
	private bool setvelocityZeto = true;

	private RectTransform rt;
	private GameObject smallCircle;
	private Button controller;
	private Rect BottomRegion = new Rect(0,0,0,0); 

	// Speeds
	private float rotationScale = 2;
	private float speedScale = 2;
	public float boostSpeed = 1000;

	//Handling
	private List<AxleInfo> axleInfos = new List<AxleInfo>(); 
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float brakeForce;

	public Vector3 centerOfMassCorrection;
	//Internal
	public bool controlled;
	public bool touchScreen;
	private bool braking;
	private bool boost = false;

	WheelCollider WheelL ;
	WheelCollider WheelR ;
	float AntiRoll = 80000.0f;
	//Touch
	private Touch lastControlTouch;
	private Touch lastScreenTouch;

	void Start () {
		playerRigidbody = GetComponent<Rigidbody> ();
		GetComponent<Rigidbody>().centerOfMass = centerOfMassCorrection;

		controlSettings = GameObject.Find ("Root").GetComponent<ScriptSettingsControls> ();
		buildControllerScript = GameObject.Find ("Root").GetComponent<ScriptBuildController> ();
		hud = GameObject.Find ("Root").GetComponent<ScriptPlayerHUD> ();

		ScriptTrashController trashContr = GameObject.Find ("Root").GetComponent<ScriptTrashController> (); // test
		trashContr.spawnTrash ("trash1", "recycable_trash_pos_1", "a"); // test
		trashContr.spawnTrash ("trash2", "recycable_trash_pos_3", "b"); // test
		trashContr.spawnTrash ("trash3", "recycable_trash_pos_2", "d"); // test

		smallCircle = GameObject.Find ("smallCircle");
		controller = GameObject.Find ("Joystick").GetComponent<Button> ();

		//touchScreen = Input.multiTouchEnabled;
		Debug.Log ("TouchEnabled: " + touchScreen);

		tempCameraDistance = controlSettings.cam_distance_s;

		rt = controller.image.rectTransform; 
		BottomRegion.width = rt.rect.width ;
		BottomRegion.height = rt.rect.height ;
		BottomRegion.position = new Vector2 (controller.gameObject.transform.position.x - rt.rect.width / 2, controller.gameObject.transform.position.y - rt.rect.height / 2);
	
		AxleInfo front = new AxleInfo ();
		front.leftWheel = GameObject.Find ("frontLeft").GetComponent<WheelCollider> ();
		front.rightWheel = GameObject.Find ("frontRight").GetComponent<WheelCollider> ();
		//front.motor = true;
		front.steering = true;
		AxleInfo back = new AxleInfo ();
		back.rightWheel = GameObject.Find ("backRight").GetComponent<WheelCollider> ();
		back.leftWheel = GameObject.Find ("backLeft").GetComponent<WheelCollider> ();
		back.motor = true;

		axleInfos.Add (front);
		axleInfos.Add (back);


	}
		
	void Update()
	{


		if (Input.GetKeyDown (KeyCode.N))
			hud.SpawnText ("ssssssssss", 10);
		if (Input.GetKeyDown (KeyCode.M))
			hud.SpawnImage ("tutorial_image_1", 10);
		if (Input.GetKeyDown (KeyCode.P))
			trashCollectedRecy[0] += 1;
		if ( Input.GetKeyDown(KeyCode.L))
			trashCollectedRecy[1] += 1;
		if ( Input.GetKeyDown(KeyCode.O))
			trashCollectedRecy[2] += 1;

		int nbTouches = Input.touchCount;
		if (nbTouches > 0) {
			for (int i = 0; i < nbTouches; i++) {
				Touch touch = Input.GetTouch (i);
				TouchPhase phase = touch.phase;
				if (BottomRegion.Contains (new Vector2 (touch.position.x, touch.position.y ))  ) { // && phase == TouchPhase.Stationary
					lastControlTouch = touch;
				}
			}
		}
	}


	void FixedUpdate()
	{
		JoystickControlsWithWheels ();
	}


	bool pressedMouse = false;
	float tempRot;
	float tempSpeed;

	void JoystickControlsWithWheels()
	{
		if (controlled)
		{
			float forceRotation = 0;
			float forceSpeed = 0;

			Vector2 controlVector;
			if (touchScreen)
				controlVector = new Vector2 (lastControlTouch.position.x, lastControlTouch.position.y);
			else
				controlVector = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			if (BottomRegion.Contains (controlVector) && Input.GetMouseButton(0)) { // && Input.GetMouseButton(0) 
				smallCircle.transform.position = controlVector;
				pressedMouse = true;
				//Debug.Log (BottomRegion.x + "  " + BottomRegion.width / 2+" "+controlVector.y);
				forceRotation = (BottomRegion.x + BottomRegion.width / 2) - controlVector.x;
				forceRotation = -forceRotation / BottomRegion.width * rotationScale;
				forceSpeed = (BottomRegion.y + BottomRegion.height / 2) - controlVector.y;
				forceSpeed = -forceSpeed / BottomRegion.height * speedScale;
				braking = false;

				Vector3 velocity = playerRigidbody.velocity; // get speed in world place ( velocity )
				Vector3 localVel = transform.InverseTransformDirection (velocity); // transform to local space so we know our velocity in relation to our orientation 

			
				if (localVel.z > 0 && forceSpeed < 0.05f) { // velocity moving u forward and u say to move back
					braking = true;
				} 
				if ( localVel.z < 0 && forceSpeed > 0.05f){ //velocity  moving u backwards and u say to move forward
					braking = true;
				}
				tempRot = forceRotation;
				tempSpeed = forceSpeed;

			} else if(Input.GetMouseButtonUp(0) )  { //
				smallCircle.transform.position = new Vector3 (BottomRegion.position.x + BottomRegion.width / 2, BottomRegion.position.y + BottomRegion.height / 2, 0);
				braking = true; 
				pressedMouse = false;
			} else if (pressedMouse) {
				forceRotation = (BottomRegion.x + BottomRegion.width / 2 ) - controlVector.x;
				forceRotation = -forceRotation / controlVector.x * rotationScale;
				forceSpeed = (BottomRegion.y + BottomRegion.height / 2) - controlVector.y;
				forceSpeed = -forceSpeed / controlVector.y * speedScale;
				forceSpeed = Mathf.Clamp (forceSpeed, -1.0f, 1.0f);
				forceRotation = Mathf.Clamp (forceRotation, -1.0f, 1.0f);
				//Debug.Log (forceSpeed + " " + forceRotation);
			}



			float motor = maxMotorTorque * forceSpeed; // Input.GetAxis("Vertical")
			float steering = maxSteeringAngle * forceRotation; //  Input.GetAxis("Horizontal")

			foreach (AxleInfo axleInfo in axleInfos)
			{
				if (axleInfo.steering)
				{
					axleInfo.leftWheel.steerAngle = steering;
					axleInfo.rightWheel.steerAngle = steering;

				}
				if (axleInfo.motor)
				{
					axleInfo.leftWheel.motorTorque = motor;
					axleInfo.rightWheel.motorTorque = motor;
				}
					
				//Braking
				if (braking)
				{
					axleInfo.leftWheel.brakeTorque = brakeForce;
					axleInfo.rightWheel.brakeTorque = brakeForce;
				}
				else
				{
					axleInfo.leftWheel.brakeTorque = 0;
					axleInfo.rightWheel.brakeTorque = 0;
				}
					

				ApplyLocalPositionToVisuals(axleInfo.leftWheel);
				ApplyLocalPositionToVisuals(axleInfo.rightWheel);

				////////////
				WheelHit hit ;
				float travelL = 1.0f;
				float travelR = 1.0f;

				bool groundedL = axleInfo.leftWheel.GetGroundHit(out hit);
				if (groundedL)
					travelL = (-axleInfo.leftWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.leftWheel.radius) / axleInfo.leftWheel.suspensionDistance;

				bool groundedR = axleInfo.rightWheel.GetGroundHit(out hit);
				if (groundedR)
					travelR = (-axleInfo.rightWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.rightWheel.radius) / axleInfo.rightWheel.suspensionDistance;

				float antiRollForce = (travelL - travelR) * AntiRoll;


				if (groundedL)
					playerRigidbody.AddForceAtPosition(axleInfo.leftWheel.transform.up * -antiRollForce, axleInfo.leftWheel.transform.position); 
				if (groundedR)
					playerRigidbody.AddForceAtPosition(axleInfo.rightWheel.transform.up * antiRollForce, axleInfo.rightWheel.transform.position); 
				if ( groundedL == false && groundedR == false) {
					playerRigidbody.AddForceAtPosition(axleInfo.leftWheel.transform.up * -100000, axleInfo.leftWheel.transform.position); 
					playerRigidbody.AddForceAtPosition(axleInfo.rightWheel.transform.up * -100000, axleInfo.rightWheel.transform.position); 
				}
				/////////////////
			}
		}
	}

	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0) {
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}

		


	IEnumerator ResumeAgentAfter( float time)
	{
		yield return new WaitForSeconds (time);
		boost = false;
	}

	public void boostPlayer(GameObject obj)
	{
		
		//playerRigidbody.AddForce (gameObject.transform.forward*boostSpeed,ForceMode.Impulse);// = motor * boostSpeed;
			//axleInfo.leftWheel.motorTorque = motor * boostSpeed;
			//axleInfo.rightWheel.motorTorque = motor * boostSpeed;

		Debug.Log ("boosting player");
	}

	public void resetTrashCollected()
	{
		for (int i = 0; i < trashCollectedRecy.Length; i++) {
			trashCollectedRecy [i] = 0;
		}

	}

	public void addTrash( int trashtype)
	{
		switch (trashtype) {
		case 0 :
			trashCollectedBio += 1;
			break;
		case 1 :
			trashCollectedRecy[0] +=1;
			break;
		case 2 :
			trashCollectedRecy[1] +=1;
			break;
		case 3 : 
			trashCollectedRecy[2] +=1;
			break;
		}
	}

	public int getRecycablesCollected( int trashNum )
	{
		int returnNum = 0;
		switch (trashNum)
		{
		case 0:
			returnNum = trashCollectedBio;
			break;
		case 1:
			returnNum = trashCollectedRecy[0];
			break;
		case 2:
			returnNum = trashCollectedRecy[1];
			break;
		case 3:
			returnNum = trashCollectedRecy[2];
			break;
		}

		return returnNum;
	}

}
