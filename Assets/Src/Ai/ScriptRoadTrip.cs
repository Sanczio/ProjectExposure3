using UnityEngine;
using System.Collections;

public class ScriptRoadTrip : MonoBehaviour {

    public GameObject _topExit;
    public GameObject _leftExit;
    public GameObject _rightExit;

    string _leftExitType;
    string _rightExitType;
    string _topExitType;

    // Use this for initialization
    void Start () {
	
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

    }

    public GameObject GetDirectionRight(GameObject lastRoad)
    {
        if (lastRoad.GetInstanceID() == _rightExit.GetInstanceID())
        {
            return _topExit;

        }

        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            return _leftExit;
        }
        if (lastRoad.GetInstanceID() == _leftExit.GetInstanceID())
        {
            return _rightExit;
        }
        print("DirectionError");
        return null;

    }

    public GameObject GetDirectionLeft(GameObject lastRoad)
    {
        if (lastRoad.GetInstanceID() == _rightExit.GetInstanceID())
        {
            return _leftExit;
        }
        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            return _rightExit;
        }
        if (lastRoad.GetInstanceID() == _leftExit.GetInstanceID())
        {
            return _topExit;
        }
        print("DirectionError");
        return null;

    }



}
