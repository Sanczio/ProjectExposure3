using UnityEngine;
using System.Collections;

public class ScriptPickUpRunAway : MonoBehaviour {

    private NavMeshAgent _agent;
	public int ID;
    public float _runAwayDistance = 1.0f;
	ScriptPlayerControls player;
	ScriptTrashController trashController;

	//private GameObject[] bio_spawns;
	//private GameObject[] recycable_spawns;

    // Use this for initialization
    void Start () {
		_agent = gameObject.GetComponent<NavMeshAgent>();
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		trashController = GameObject.Find ("Root").GetComponent<ScriptTrashController> ();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivatePickUp(Vector3 playerPosition)
    {
		if ( _agent != null && gameObject != null) 
		_agent.destination = DecideWhereToGo (playerPosition);
    }

    Vector3 DecideWhereToGo(Vector3 playerPosition)
    { 
		Vector3 goalPosition ;
        Transform tempTransform = this.GetComponent<Transform>();
        Vector3 normVector = -1 * (playerPosition - tempTransform.position);
        goalPosition = tempTransform.position + normVector.normalized * _runAwayDistance;
        return goalPosition;
    }

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "PlayerCrashCollider") {
			ScriptTrashSpawnPoint spawnTempRecy = trashController.getRecySpawns () [ID].GetComponent<ScriptTrashSpawnPoint> ();
			//ScriptTrashSpawnPoint spawnTempBio = trashController.getBioSpawns () [ID].GetComponent<ScriptTrashSpawnPoint> ();

			if (gameObject.name == "recycable_trash_a(Clone)") {
				player.addTrash (1);
				Destroy (gameObject);
				spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.name == "recycable_trash_b(Clone)") {
				player.addTrash (2);
				Destroy (gameObject);
				spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.name == "recycable_trash_c(Clone)") {
				player.addTrash (3);
				Destroy (gameObject);
				spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.name == "bio_trash(Clone)") {
				player.addTrash (0);
				Destroy (gameObject);
				//spawnTempBio.makeAvailable (true);
			}	
		}
	}
		

}
