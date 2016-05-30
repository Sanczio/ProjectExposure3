using UnityEngine;
using System.Collections;

public class ScriptRoadRound : MonoBehaviour {

    public GameObject _topExit;
    public GameObject _leftExit;
    public GameObject _rightExit;

    string _leftExitType;
    string _rightExitType;
    string _topExitType;

    // Use this for initialization
    void Start()
    {

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

    public GameObject GetDirectionLeft()
    {

        return _leftExit;
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
        print("DirectionError");
        return null;

    }

    public bool ShouldTurnRight(GameObject lastRoad)
    {
        bool shouldTurnRound = true;
        if (lastRoad.GetInstanceID() == _topExit.GetInstanceID())
        {
            shouldTurnRound = false;
        }
        return shouldTurnRound;
    }
}
