using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Place the script in the Camera-Control group in component menu
[AddComponentMenu("Camera-Control/ScriptCameraControl")]

public class ScriptCameraControl : MonoBehaviour {

	// target to follow
	private Transform target;
	// distance in the x-z plane to the target
	private float distance ;
	// the height we want the camera to be above the target
	private float height ;
	// how much we:
	private float heightDamping ;
	private float rotationDamping ;

	private float intensityGrayscale;
	private Material material;
	//private List<GameObject> areasInGrayscale;


	private ScriptSettingsControls controlSettings;

	void Awake()
	{
		//material = new Material (Shader.Find ("Hidden/grayscaleShader"));
	}

	void Start()
	{
		controlSettings = GameObject.Find ("Root").GetComponent<ScriptSettingsControls> ();
		target = controlSettings.cam_target_s;
		distance = controlSettings.cam_distance_s;
		height = controlSettings.cam_height_s;
		heightDamping = controlSettings.cam_heightDamping_s;
		rotationDamping = controlSettings.cam_rotationDamping_s;
	}

	void Update()
	{
		distance = controlSettings.cam_distance_s;
		height = controlSettings.cam_height_s;
	}

	void LateUpdate ()
	{
		// stop if no target
		if (!target)
			return;

		// calculate current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// damp the rotation around the y - axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle,wantedRotationAngle,rotationDamping * Time.deltaTime);
		// damp height
		currentHeight = Mathf.Lerp(currentHeight,wantedHeight,heightDamping * Time.deltaTime);

		// convert angles to rotation
		Quaternion currentRotation = Quaternion.Euler(0,currentRotationAngle,0);

		// set the position of the camera onthe x - z plane to :
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		//set the height of the camera
		transform.position = new Vector3(transform.position.x , currentHeight , transform.position.z);

		//always look at the target
		transform.LookAt (target);
	}

	void OnRenderImage( RenderTexture source , RenderTexture destination)
	{
		if (intensityGrayscale == 0) {
			Graphics.Blit (source, destination);
			return;
		}

		material.SetFloat ("_bwBlend", intensityGrayscale);
		Graphics.Blit (source, destination, material);
	}

	public void setGrayscale( float intensity)
	{
		intensityGrayscale = intensity;
	}
}
