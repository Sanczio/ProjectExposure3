using UnityEngine;
using System.Collections;

public class ScriptPickUpSpawner : MonoBehaviour {

    public GameObject _pickUpMovementSpeed;
    public GameObject _pickUpShooting;

	// Use this for initialization
	void Start () {
	
	}
	
    public void CallPickUpSpawner(string pickUpType, string placeName)
    {
        GameObject tempPlace = GameObject.Find(placeName);
        switch (pickUpType)
        {
            case "MovementBoost":
                SpawnPickUp(_pickUpMovementSpeed, tempPlace);
                break;

            case "ShootingBoost":
                SpawnPickUp(_pickUpShooting, tempPlace);
                break;

        }
            
    }

    void SpawnPickUp(GameObject prefab, GameObject placeToSpawn)
    {
        GameObject spawnPickUp = Instantiate(prefab, placeToSpawn.transform.position, Quaternion.identity) as GameObject;

    }

}
