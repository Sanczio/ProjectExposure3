using UnityEngine;
using System.Collections;

public class ScriptCarSpawner : MonoBehaviour {

    public GameObject _civilCar;
    public GameObject _enemyCar;

    public GameObject[] _simpleRoadList;

    // Use this for initialization
    void Start () {
        _simpleRoadList = GameObject.FindGameObjectsWithTag("RoadSimple");
    }
	
    public void CallCarSpawner(string carName, string carType, string roadName, string roadSide, bool activeAfterStart)
    {
        switch (carType)
        {
            case "CivilCar":
                SpawnCivilCar(carName, roadName, roadSide, activeAfterStart);
                break;
            case "EnemyCar":
                SpawnEnemyCar(carName, roadName, roadSide, activeAfterStart);
                break;
        }

    }

    void SpawnCivilCar(string carName, string roadName, string roadSide, bool activeAfterStart)
    {
        //print(roadName);
        GameObject currentRoad = GameObject.Find(roadName);
        //print(currentRoad.gameObject.name);
        ScriptRoadSimple tempRoadScript = currentRoad.GetComponent<ScriptRoadSimple>();
        GameObject tempWaypoint = tempRoadScript._listWaypointsRight[0];
        switch (roadSide)
        {
            case "Right":
                tempWaypoint = tempRoadScript._listWaypointsRight[0];
                break;

            case "Left":
                tempWaypoint = tempRoadScript._listWaypointsLeft[0];
                break;
        }
        GameObject correctWaypoint = tempWaypoint;
        GameObject spawnCar = Instantiate(_civilCar, correctWaypoint.transform.position, Quaternion.identity) as GameObject;
        ScriptCivilCar tempCarScript = spawnCar.GetComponent<ScriptCivilCar>();
        tempCarScript.AfterSpawn(currentRoad, roadSide, activeAfterStart);

    }

    void SpawnEnemyCar(string carName, string roadName, string roadSide, bool activeAfterStart)
    {
        GameObject currentRoad = GameObject.Find(roadName);
        ScriptRoadSimple tempRoadScript = currentRoad.GetComponent<ScriptRoadSimple>();
        GameObject tempWaypoint = tempRoadScript._listWaypointsRight[0];
        switch (roadSide)
        {
            case "Right":
                tempWaypoint = tempRoadScript._listWaypointsRight[0];
                break;

            case "Left":
                tempWaypoint = tempRoadScript._listWaypointsLeft[0];
                break;
        }
        GameObject correctWaypoint = tempWaypoint;
        GameObject spawnCar = Instantiate(_enemyCar, correctWaypoint.transform.position, Quaternion.identity) as GameObject;
        ScriptCivilCar tempCarScript = spawnCar.GetComponent<ScriptCivilCar>();
        tempCarScript.AfterSpawn(currentRoad, roadSide, activeAfterStart);

    }
}
