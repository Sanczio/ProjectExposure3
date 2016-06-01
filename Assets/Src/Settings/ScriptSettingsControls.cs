using UnityEngine;
using System.Collections;

public class ScriptSettingsControls : MonoBehaviour {

	//--------CameraControlSettings-----START
	// target to follow
	public Transform cam_target_s;
	// distance in the x-z plane to the target
	public float cam_distance_s = 10.0f;
	// the height we want the camera to be above the target
	public float cam_height_s = 5.0f;
	// how much we:
	public float cam_heightDamping_s = 2.0f;
	public float cam_rotationDamping_s = 3.0f;
	//--------CameraControlSettings-----END

	//--------PlayerControlSettings-----START
	public float player_speed = 1600;
	public float player_max_steering_angle = 35;
	public float player_brake_force = 10000;
	public float player_boost = 20;
	public bool  player_touchscreen = false;


	//--------PlayerControlSettings-----END
}
