using UnityEngine;
using System.Collections;

public class ScriptRoadSimple : MonoBehaviour {

	public GameObject[] _listWaypointsRight = new GameObject[7];  
	public GameObject[] _listWaypointsLeft = new GameObject[7];

    public GameObject _leftExit;
	public GameObject _rightExit;

    string _leftExitType;
    string _rightExitType;
	// Use this for initialization
	void Start () {
        GetExitTypes();

    }
	
    void GetExitTypes()
    {
        if (_leftExit)
        {
            string _leftExitTag = _leftExit.tag;
            switch (_leftExitTag)
            {
                case "Simple":
                    {
                        _leftExitType = _leftExitTag;
                        break;
                    }
                case "Square":
                    {
                        _leftExitType = _leftExitTag;
                        break;
                    }
                case "Default":
                    {
                        print("Road Type Error: rod type not found");
                        break;
                    }
            }
        }
        if (_rightExit)
        {
            string _rightExitTag = _rightExit.tag;
            switch (_rightExitTag)
            {
                case "Simple":
                    {
                        _leftExitType = _rightExitTag;
                        break;
                    }
                case "Square":
                    {
                        _leftExitType = _rightExitTag;
                        break;
                    }
                case "Default":
                    {
                        print("Road Type Error: rod type not found");
                        break;
                    }
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsThereNextWaypoint(string roadSide, int currentWaypoint)
    {
        bool tempAsnwer = true;
        if (roadSide == "Left")
        {
            if (_listWaypointsLeft.Length > currentWaypoint)
            {
                tempAsnwer = true;
            }
            else tempAsnwer = false;
        }
        if (roadSide == "Right")
        {
            if (_listWaypointsRight.Length > currentWaypoint)
            {
                tempAsnwer = true;
            }
            else tempAsnwer = false;
        }

        return tempAsnwer;
    }

    public GameObject GetWaypoint(string roadSide, int currentWaypoint)
    {
        if (roadSide == "Left")
        {
           return _listWaypointsLeft[currentWaypoint];
        }
        if (roadSide == "Right")
        {
            return _listWaypointsRight[currentWaypoint];

        }
        return null;
    }

    public bool CheckIfThereIsExit(string roadSide)
    {
        bool tempAsnwer = true;
        if (roadSide == "Left")
        {
            if (_leftExit)
            {
                tempAsnwer = true;
            }
            else tempAsnwer = false;
        }
        if (roadSide == "Right")
        {
            if (_rightExit)
            {
                tempAsnwer = true;
            }
            else tempAsnwer = false;
        }

        return tempAsnwer;
    }

    public GameObject GetExit(string roadSide)
    {
        if (roadSide == "Left")
        {
            return _leftExit;
        }
        if (roadSide == "Right")
        {
            return _rightExit;

        }
        return null;
    }

    public string GetNextRoadSide(GameObject lastRoad)
    {

        string tempAsnwerRoadSide = "";
        if (lastRoad == _leftExit)
        {
            tempAsnwerRoadSide = "Right";
        } else if (lastRoad == _rightExit)
        {
            tempAsnwerRoadSide = "Left";
        }


        return tempAsnwerRoadSide;
    }

}
