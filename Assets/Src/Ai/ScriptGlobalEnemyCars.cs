using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptGlobalEnemyCars : MonoBehaviour {

    public int _maxEnemyCars = 0;
    public GameObject _enemyCarPrefab;
    public float _repeatTime = 10.0f;
    public float _startSpawningAfter = 1.0f;
    public bool _maxCarNumberReached = false;
    List<GameObject> _enemyCarsOnSceneList = new List<GameObject>();
    public GameObject[] _simpleRoadList;


    // Use this for initialization
    void Start () {
        _simpleRoadList = GameObject.FindGameObjectsWithTag("RoadSimple");

        InvokeRepeating("SpawnEnemyCar", _startSpawningAfter, _repeatTime);
    }
	
	// Update is called once per frame
	void Update () {
	    if (_enemyCarsOnSceneList.Count < _maxEnemyCars)
        {
            _maxCarNumberReached = false;
        } else
            {
                _maxCarNumberReached = true;
            }
	}

    void SpawnEnemyCar()
    {
        if (_maxCarNumberReached != true)
        {
            GameObject currentRoad = _simpleRoadList[GetRandomRoad()];
            ScriptRoadSimple tempRoadScript = currentRoad.GetComponent<ScriptRoadSimple>();
            GameObject tempWaypoint = tempRoadScript._listWaypointsRight[0];
            GameObject spawnCar = Instantiate(_enemyCarPrefab, tempWaypoint.transform.position, Quaternion.identity) as GameObject;
            ScriptCivilCar tempCarScript = spawnCar.GetComponent<ScriptCivilCar>();
            //tempCarScript.AfterSpawn(currentRoad, "Right");
        }
        

    }

    int GetRandomRoad()
    {
        int tempRandomMax = _simpleRoadList.Length;
        int choosenRoad = Random.Range(0, tempRandomMax);

        return choosenRoad;
    }

}
