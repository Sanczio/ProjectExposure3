using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptGlobalCivilCars : MonoBehaviour {

    public GameObject[] _simpleRoadList;
    public GameObject _carPrefab;


    public int _maxCivilCars = 0;
    public float _repeatTime = 10.0f;
    public float _startSpawningAfter = 1.0f;
    public bool _maxCarNumberReached = false;
    List<GameObject> _enemyCarsOnSceneList = new List<GameObject>();
    // Use this for initialization
    void Start () {
        _simpleRoadList = GameObject.FindGameObjectsWithTag("RoadSimple");
        print(_simpleRoadList.Length);

		InvokeRepeating("SpawnCivilCar", _startSpawningAfter, _repeatTime);
//		for (int i = 0; i < 20; i++) 
//		{
//			SpawnCivilCar ();
//		}
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemyCarsOnSceneList.Count < _maxCivilCars)
        {
            _maxCarNumberReached = false;
        }
        else
        {
            _maxCarNumberReached = true;
        }
    }

    void SpawnCivilCar()
    {   
        if (_maxCarNumberReached != true)
        {
            GameObject currentRoad = _simpleRoadList[GetRandomRoad()];
            ScriptRoadSimple tempRoadScript = currentRoad.GetComponent<ScriptRoadSimple>();
            GameObject tempWaypoint = tempRoadScript._listWaypointsRight[0];
            GameObject spawnCar = Instantiate(_carPrefab, tempWaypoint.transform.position, Quaternion.identity) as GameObject;
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
