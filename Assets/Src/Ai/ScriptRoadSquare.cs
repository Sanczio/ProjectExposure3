using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptRoadSquare : MonoBehaviour {

    public GameObject _leftExit;
    public GameObject _rightExit;
    public GameObject _topExit;
    public GameObject _downExit;

    string _leftExitType;
    string _rightExitType;
    string _topExitType;
    string _downExitType;

    public GameObject[] _listWaypoints = new GameObject[1];

    // Use this for initialization
    void Start () {
        GetExitTypes();
    }

    void GetExitTypes()
    {
        if (_leftExit)
        {
            _leftExitType = _leftExit.tag;
        }
        if (_rightExit)
        {
            _rightExitType = _rightExit.tag;
        }
        if (_topExit)
        {
            _topExitType = _topExit.tag;
        }
        if (_downExit)
        {
            _downExitType = _downExit.tag;
        }
        
        
        
        
    }

    public bool IsThereNextWaypoint(int currentWaypoint)
    {
        bool tempAsnwer = true;
        if (_listWaypoints.Length > currentWaypoint)
        {
            tempAsnwer = true;
        }
        else { tempAsnwer = false; }
        

        return tempAsnwer;
    }

    public GameObject GetExit(GameObject lastRoad)
    {
        if (_leftExit)
        {
            return _leftExit;
        }
        if (_rightExit)
        {
            return _rightExit;

        }
        if (_topExit)
        {
            return _topExit;

        }
        if (_downExit)
        {
            return _downExit;

        }
        return null;
    }

    public bool CanTurnRight(GameObject currentRoad)
    {
        if (currentRoad == _rightExit)
        {
            if (_topExit)
            {
                return true;
            }
        }
        if (currentRoad == _topExit)
        {

        }
        if (currentRoad == _downExit)
        {

        }
        if (currentRoad == _leftExit)
        {

        }

        return true;
    }

  public GameObject GetDirectionRight(GameObject lastRoad)
    {
        //print(lastRoad.GetInstanceID());
        //print(_rightExit.GetInstanceID());
        //print(_topExit.GetInstanceID());
        //print(_downExit.GetInstanceID());
        //print(_leftExit.GetInstanceID());
        if (lastRoad.GetInstanceID() == _rightExit.GetInstanceID())
        {
            return _topExit;
            
        }
        
        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            return _leftExit;
        }
        if (lastRoad.GetInstanceID() == _downExit.GetInstanceID())
        {
            return _rightExit;
        }
        if (lastRoad.GetInstanceID() == _leftExit.GetInstanceID())
        {
            return _downExit;
        }
        print("DirectionError");
        return null;

    }

    public GameObject GetDirectionLeft(GameObject lastRoad)
    {
        if (lastRoad.GetInstanceID() == _rightExit.GetInstanceID())
        {
            return _downExit;
        }
        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            return _rightExit;
        }
        if (lastRoad.GetInstanceID() == _downExit.GetInstanceID())
        {
            return _leftExit;
        }
        if (lastRoad.GetInstanceID() == _leftExit.GetInstanceID())
        {
            return _topExit;
        }
        print("DirectionError");
        return null;

    }

    public GameObject GetDirectionStrait(GameObject lastRoad)
    {
        if (lastRoad.GetInstanceID() == _rightExit.GetInstanceID())
        {
            return _leftExit;
        }
        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            return _downExit;
        }
        if (lastRoad.GetInstanceID() == _downExit.GetInstanceID())
        {
            return _topExit;
        }
        if (lastRoad.GetInstanceID() == _leftExit.GetInstanceID())
        {
            return _rightExit;
        }
        print("DirectionError");
        return null;

    }


    // Update is called once per frame
    void Update () {
	
	}
}
