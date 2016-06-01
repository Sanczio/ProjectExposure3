using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScriptAssignmentController : MonoBehaviour {

	ScriptPlayerControls player;
	ScriptSettingsGameplay gameplaySettings;
	ScriptPlayerHUD hud;
	private int assignmentNr = 0;
	private bool collidingWithAssignmentGiver = false;

	private Text timer_text;
	private Text trash_a_text;
	private Text trash_b_text;
	private Text trash_c_text;
	private Text trash_bio_text;

	private GameObject canvas;
	private GameObject trash_a_prefab;
	private GameObject trash_b_prefab;
	private GameObject trash_c_prefab;




	private float time_left;
	// Use this for initialization
	private int[][] assignment = {
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
	};

	private ScriptCameraControl cameraScript;
	private ScriptArea area_1;
	private ScriptArea area_2;
	private ScriptArea area_3;
	private ScriptArea tutorial;

	private int[] player_trash = {0,0,0,0}; // 1 - a trash ; 2 - b trash ; 3 - c trash , 0 - bio waste
	private int[] time_limits = new int[3];

	void Start () {
		hud = GameObject.Find ("Root").GetComponent<ScriptPlayerHUD> ();
		canvas = GameObject.Find ("Canvas");
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		timer_text = GameObject.Find("timer_text").GetComponent<Text>();
		//trash_a_text = GameObject.Find("trash_a_text").GetComponent<Text>();
		//trash_b_text = GameObject.Find("trash_b_text").GetComponent<Text>();
		//trash_c_text = GameObject.Find("trash_c_text").GetComponent<Text>();
		//trash_bio_text = GameObject.Find("trash_bio_text").GetComponent<Text>();
//		tutorial = GameObject.Find ("Tutorial").GetComponent<ScriptArea> ();
//		area_1 = GameObject.Find ("Area1").GetComponent<ScriptArea> ();
//		area_2 = GameObject.Find ("Area2").GetComponent<ScriptArea> ();
//		area_3 = GameObject.Find ("Area3").GetComponent<ScriptArea> ();
		gameplaySettings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();

		time_limits[0] = gameplaySettings.assignment_1_time;
		time_limits[1] = gameplaySettings.assignment_2_time;
		time_limits[2] = gameplaySettings.assignment_3_time;

		assignment[0][0] = gameplaySettings.assignment0_a_trash;
		assignment[0][1] = gameplaySettings.assignment0_b_trash;
		assignment[0][2] = gameplaySettings.assignment0_c_trash;

		assignment[1][0] = gameplaySettings.assignment1_a_trash;
		assignment[1][1] = gameplaySettings.assignment1_b_trash;
		assignment[1][2] = gameplaySettings.assignment1_c_trash;

		assignment[2][0] = gameplaySettings.assignment2_a_trash;
		assignment[2][1] = gameplaySettings.assignment2_b_trash;
		assignment[2][2] = gameplaySettings.assignment2_c_trash;

		assignment[3][0] = 20;
		assignment[3][1] = 0;
		assignment[3][2] = 0;

		time_left = Time.time + time_limits[0];


		trash_a_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_a_image");
		trash_b_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_b_image");
		trash_c_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_c_image");

//		trash_a_text.gameObject.transform.position = new Vector3 (Screen.width / 10, trash_a_text.gameObject.transform.position.y, 0);
//		trash_b_text.gameObject.transform.position = new Vector3 (Screen.width / 10, trash_b_text.gameObject.transform.position.y, 0);
//		trash_c_text.gameObject.transform.position = new Vector3 (Screen.width / 10, trash_c_text.gameObject.transform.position.y, 0);
//		trash_bio_text.gameObject.transform.position = new Vector3 (Screen.width / 10, trash_bio_text.gameObject.transform.position.y, 0);
		timer_text.gameObject.transform.position = new Vector3 (timer_text.gameObject.transform.position.x, Screen.height / 8 * 7 , 0);

		spawnImages ();
	}
	
	// Update is called once per frame
	void Update () {


		drawHud ();



		if (assignmentNr < 4 && assignment [assignmentNr] [0] <= player_trash [1] && assignment [assignmentNr] [1] <= player_trash [2] && assignment [assignmentNr] [2] <= player_trash [3] && collidingWithAssignmentGiver == true) {
			Debug.Log ("assignment completed");
			player.resetTrashCollected ();
			deleteImages ();
			assignmentNr += 1;
			hud.AddEnergy (25);
			time_left = time_limits [assignmentNr];
			spawnImages ();
		}
		if( assignment[3][0] <= player_trash[0] )
		{
			hud.AddEnergy (25);
		}
		if (time_left <= 0 && assignmentNr < 3) {
			assignmentNr += 1;
			player.resetTrashCollected ();
			deleteImages ();
			spawnImages ();
			time_left = time_limits [assignmentNr];
		}
			
		if (assignmentNr > 0) {
			area_1.gameObject.GetComponent<Collider>().isTrigger = true;
			tutorial.setGrayscaledArea (false);
		}
		if (assignmentNr > 1) {
			area_2.gameObject.GetComponent<Collider>().isTrigger = true;
			area_1.setGrayscaledArea (false);
		}
		if (assignmentNr > 2) {
			area_3.gameObject.GetComponent<Collider>().isTrigger = true;
			area_2.setGrayscaledArea (false);
		}
				
		collidingWithAssignmentGiver = false;


	}

	private List<GameObject> a_trash_images = new List<GameObject>();
	private List<GameObject> b_trash_images = new List<GameObject>();
	private List<GameObject> c_trash_images = new List<GameObject>();

	private void spawnImages()
	{
		if (assignment [assignmentNr] [0] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [0]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_a_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 9) , trash_a_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				a_trash_images.Add (temp_image);
			}
		}

		if (assignment [assignmentNr] [1] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [1]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_b_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 7) , trash_b_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				b_trash_images.Add (temp_image);
			}
		}

		if (assignment [assignmentNr] [2] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [2]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_c_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 5) , trash_c_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				c_trash_images.Add (temp_image);
			}
		}

			
	}

	private void deleteImages()
	{
		for (int i = 0; i < a_trash_images.Count; i++) {
			Destroy (a_trash_images [i]);
		}
		a_trash_images.Clear();
		player_trash [1] = 0;

		for (int j = 0; j < b_trash_images.Count; j++) {
			Destroy (b_trash_images [j]);
		}
		b_trash_images.Clear();
		player_trash [2] = 0;

		for (int k = 0; k < c_trash_images.Count; k++) {
			Destroy (c_trash_images [k]);
		}
		c_trash_images.Clear();
		player_trash [3] = 0;
	}

	private void colorImage(int trashNr)
	{
		
		if (trashNr == 1 && player_trash[1] <= assignment[assignmentNr][0] ) {
			a_trash_images[ player_trash[1] - 1 ].GetComponent<Button>().interactable = true;
		}
		if (trashNr == 2 && player_trash[2] <= assignment[assignmentNr][1] ) {
			b_trash_images[ player_trash[2] - 1 ].GetComponent<Button>().interactable = true;
		}
		if (trashNr == 3 && player_trash[3] <= assignment[assignmentNr][2] ) {
			c_trash_images[ player_trash[3] - 1 ].GetComponent<Button>().interactable = true;
		}
	}

	private void drawHud()
	{
		player_trash [0] = player.getRecycablesCollected (0);

		//player_trash [2] = player.getRecycablesCollected (2);
		//player_trash [3] = player.getRecycablesCollected (3);


		//trash_a_text.text = trash_b_text.text = trash_c_text.text = trash_bio_text.text = "";
		timer_text.text = Mathf.RoundToInt(time_left).ToString ()+" s left";
		if ( time_left > 0 )
			time_left -= Time.deltaTime;

		//
		if (player_trash [1] <= assignment [assignmentNr] [0] && player_trash [1] < player.getRecycablesCollected (1)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [1] = player.getRecycablesCollected (1);
			colorImage (1);
		}
		if (player_trash [2] <= assignment [assignmentNr] [1] && player_trash [2] < player.getRecycablesCollected (2)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [2] = player.getRecycablesCollected (2);
			colorImage (2);
		}
		if (player_trash [3] <= assignment [assignmentNr] [2] && player_trash [3] < player.getRecycablesCollected (3)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [3] = player.getRecycablesCollected (3);
			colorImage (3);
		}
		//	

//		if (assignment [assignmentNr] [0] > 0) {
//			trash_a_text.text = "Trash A: " + player_trash [1].ToString () + "/" + assignment [assignmentNr] [0].ToString ();
//		}
//			
//		if (assignment [assignmentNr] [1] > 0)
//			trash_b_text.text = "Trash B: " + player_trash [2].ToString () + "/" + assignment [assignmentNr] [1].ToString ();
//		if (assignment [assignmentNr] [2] > 0)
//			trash_c_text.text = "Trash C: " + player_trash [3].ToString () + "/" + assignment [assignmentNr] [2].ToString ();
		//if (assignment [assignmentNr] [0] > 0)
		//	trash_bio_text.text = "Bio waste: " + player_trash [0].ToString () + "/" + assignment [3] [0].ToString ();
	}

	public void setDeliveringGoods(int giverNum) // 0 - tuto , 1 - area1 , etc.
	{
		if (assignmentNr == giverNum) {
			collidingWithAssignmentGiver = true;
		}
	}

	public int getAssignmentNr()
	{
		return assignmentNr;
	}
}
