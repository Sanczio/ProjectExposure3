using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptTrashController : MonoBehaviour {

	private float spawnIntervalBio;
	private float spawnIntervalRecycable ;
	private float startSpawningTrashAfter ;

	private GameObject bio_spawn_prefab;
	private string bio_spawn_name;
	private List<GameObject> bio_spawns;

	public GameObject[] recycable_spawn_prefabs ; //= {null,null,null}
	private string recy_spawn_name;
	private List<GameObject> recycable_spawns;

	private int recyTrashOnScene = 0;
	private int bioTrashOnScene = 0;

	private ScriptAssignmentController assignmentController;

	private ScriptArea area;
	private ScriptArea area_1;
	private ScriptArea area_2;
	private ScriptArea area_3;
	private ScriptArea tutorial;


	void Start () {
		ScriptSettingsGameplay gameplaySettings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();
		assignmentController = GameObject.Find ("Root").GetComponent<ScriptAssignmentController> ();
		tutorial = GameObject.Find ("Tutorial").GetComponent<ScriptArea> ();
		area_1 = GameObject.Find ("Area1").GetComponent<ScriptArea> ();
		area_2 = GameObject.Find ("Area2").GetComponent<ScriptArea> ();
		area_3 = GameObject.Find ("Area3").GetComponent<ScriptArea> ();
		//tutorial_area = GameObject.Find ("Tutorial");
		spawnIntervalBio = gameplaySettings.trash_spawnIntervalBio;
		spawnIntervalRecycable = gameplaySettings.trash_spawnIntervalRecycable;
		startSpawningTrashAfter = gameplaySettings.trash_startSpawningTrashAfter;
		bio_spawn_name = gameplaySettings.prefab_n_spawn_bio;
		recy_spawn_name = gameplaySettings.prefab_n_spawn_recy;

		//string[] prefabNames = { "prefabs/"+recy_spawn_name+"_a" ,  "prefabs/"+recy_spawn_name+"_b" ,  "prefabs/"+recy_spawn_name+"_c" };

		bio_spawn_prefab = (GameObject)Resources.Load("prefabs/"+bio_spawn_name);
		//recycable_spawn_prefabs[0] = (GameObject)Resources.Load(prefabNames[0]); // 
		//recycable_spawn_prefabs[1] = (GameObject)Resources.Load("prefabs/"+recy_spawn_name+"_b"); // 
		//recycable_spawn_prefabs[2] = (GameObject)Resources.Load("prefabs/"+recy_spawn_name+"_c"); // 

		//bio_spawns = new GameObject[100];
		//recycable_spawns = new GameObject[100];

		//if (bio_spawns == null)
		//	bio_spawns = GameObject.FindGameObjectsWithTag ("bio_spawn");
		//if (recycable_spawns == null)
		//	recycable_spawns = GameObject.FindGameObjectsWithTag ("recycable_spawn");




		//if (bio_spawns.Length > 0)
			InvokeRepeating("SpawnBioTrash" , startSpawningTrashAfter ,spawnIntervalBio);
		//if (recycable_spawns.Length > 0)
			InvokeRepeating ("SpawnRecycableTrash", startSpawningTrashAfter ,spawnIntervalRecycable);
	}

	void Update()
	{
		if (assignmentController.getAssignmentNr () == 0)
			area = tutorial;
		if (assignmentController.getAssignmentNr () == 1)
			area = area_1;
		if (assignmentController.getAssignmentNr () == 2)
			area = area_2;
		if (assignmentController.getAssignmentNr () == 3)
			area = area_3;
	}

	void SpawnBioTrash()
	{
		bio_spawns = area.getBioSpawns();
		int random = Random.Range (0, bio_spawns.Count );

		ScriptTrashSpawnPoint spawnTemp = bio_spawns [random].GetComponent<ScriptTrashSpawnPoint> ();
		GameObject tempTrashObj;
		if (spawnTemp.checkIfAvailable ()) {
			tempTrashObj = (GameObject)Instantiate (bio_spawn_prefab, bio_spawns[random].transform.position, bio_spawns[random].transform.rotation);
			spawnTemp.makeAvailable (false);
			ScriptPickUpRunAway tempTrash = tempTrashObj.GetComponent<ScriptPickUpRunAway> ();
			tempTrash.ID = random;
		}


	}

	void SpawnRecycableTrash()
	{
		recycable_spawns = area.getRecySpawns();
		int random = Random.Range (0, recycable_spawns.Count );
		int randomTrash = Random.Range (0, recycable_spawn_prefabs.Length);

		ScriptTrashSpawnPoint spawnTemp = recycable_spawns [random].GetComponent<ScriptTrashSpawnPoint> ();
		GameObject tempTrashObj;
		if (spawnTemp.checkIfAvailable ()) {
			tempTrashObj = (GameObject)Instantiate (recycable_spawn_prefabs[randomTrash], recycable_spawns[random].transform.position, recycable_spawns[random].transform.rotation);
			spawnTemp.makeAvailable (false);
			ScriptPickUpRunAway tempTrash = tempTrashObj.GetComponent<ScriptPickUpRunAway> ();
			tempTrash.ID = random;
		}
			
	}


	public List<GameObject> getBioSpawns()
	{
		return bio_spawns;
	}

	public List<GameObject> getRecySpawns()
	{
		return recycable_spawns;
	}
}
