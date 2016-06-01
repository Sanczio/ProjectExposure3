using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScriptTrashController : MonoBehaviour {

	private float spawnIntervalBio;
	private float spawnIntervalRecycable ;
	private float startSpawningTrashAfter ;

	private GameObject bio_spawn_prefab;
	private GameObject[] recycable_spawn_prefabs = new GameObject[3] ; //= {null,null,null}


	private List<GameObject> recycable_spawns;
	private List<GameObject> bio_spawns;
	private List<GameObject> trash_onscene = new List<GameObject>();

	private int recyTrashOnScene = 0;
	private int bioTrashOnScene = 0;

	private ScriptAssignmentController assignmentController;



	void Start () {
		ScriptSettingsGameplay gameplaySettings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();
		assignmentController = GameObject.Find ("Root").GetComponent<ScriptAssignmentController> ();



		bio_spawn_prefab = (GameObject)Resources.Load("prefabs/bio_trash");
		recycable_spawn_prefabs[0] = (GameObject)Resources.Load("prefabs/recycable_trash_a"); // 
		recycable_spawn_prefabs[1] = (GameObject)Resources.Load("prefabs/recycable_trash_b"); // 
		recycable_spawn_prefabs[2] = (GameObject)Resources.Load("prefabs/recycable_trash_c"); // 


		recycable_spawns = GameObject.FindGameObjectsWithTag ("recycable_spawn").ToList();
		Debug.Log (recycable_spawns.Count);



	}


	public void spawnTrash ( string nameOfTrash , string nameOfSpawn , string typeOfTrash )
	{
		GameObject tempTrashObj = recycable_spawns[0];
		GameObject tempSpawnObj = recycable_spawns[0];


		foreach (GameObject spawn in recycable_spawns) {
			if (spawn.name == nameOfSpawn) {
				tempSpawnObj = spawn;
				break;
			}
		}

		switch (typeOfTrash) {
		case "a":
			tempTrashObj = (GameObject)Instantiate (recycable_spawn_prefabs [0], tempSpawnObj.transform.position, tempSpawnObj.transform.rotation);
			tempTrashObj.name = nameOfTrash;
			trash_onscene.Add (tempTrashObj);
			break;
		case "b":
			tempTrashObj = (GameObject)Instantiate (recycable_spawn_prefabs [1], tempSpawnObj.transform.position, tempSpawnObj.transform.rotation);
			tempTrashObj.name = nameOfTrash;
			trash_onscene.Add (tempTrashObj);
			break;
		case "c":
			tempTrashObj = (GameObject)Instantiate (recycable_spawn_prefabs [2], tempSpawnObj.transform.position, tempSpawnObj.transform.rotation);
			tempTrashObj.name = nameOfTrash;
			trash_onscene.Add (tempTrashObj);
			break;
		case "d":
			tempTrashObj = (GameObject)Instantiate (bio_spawn_prefab, tempSpawnObj.transform.position, tempSpawnObj.transform.rotation);
			tempTrashObj.name = nameOfTrash;
			trash_onscene.Add (tempTrashObj);
			break;
		}

	}



	void Update()
	{
//		if (assignmentController.getAssignmentNr () == 0)
//			area = tutorial;
//		if (assignmentController.getAssignmentNr () == 1)
//			area = area_1;
//		if (assignmentController.getAssignmentNr () == 2)
//			area = area_2;
//		if (assignmentController.getAssignmentNr () == 3)
//			area = area_3;
	}




	public List<GameObject> getBioSpawns()
	{
		return bio_spawns;
	}

	public List<GameObject> getRecySpawns()
	{
		return recycable_spawns;
	}
	public List<GameObject> getTrashOnScene()
	{
		return trash_onscene;
	}
	public void removeTrash( GameObject trash )
	{
		trash_onscene.Remove (trash);
	}
}
