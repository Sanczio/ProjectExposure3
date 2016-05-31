using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptCivilCar : MonoBehaviour {


    private NavMeshAgent _agent;

    public GameObject _currentRoad;
    private string _currentRoadType;
    public string _roadSide;
    private int _currentWaypointNumber = 0;
    private ScriptRoadSimple _scriptRoadSimple;
    private ScriptRoadSquare _scriptRoadSquare;
    private ScriptRoadTrip _scriptRoadTrip;
    private ScriptRoadRound _scriptRoadRound;
    private GameObject _currentWaypoint;
    public GameObject _lastRoad;
    List<GameObject> _squareDirectionList = new List<GameObject>();
    List<GameObject> _tripleDirectionList = new List<GameObject>();
    List<GameObject> _roundDirectionList = new List<GameObject>();
    bool _canMove = false;

    private bool _hittedByPlayer = false;

	private bool _followPlayer = false;

    void Start()
    {
        
        _agent = this.GetComponent<NavMeshAgent>();
        //GetRoadScript(_currentRoad);
        //GetNextWaypoint();
    }

    public void AfterSpawn(GameObject currentRoad, string direction)
    {
        _currentRoad = currentRoad;
        _roadSide = direction;
        GetRoadScript(_currentRoad);
        GetNextWaypoint();

    }

    void GetRoadScript(GameObject currentRoad)
    {
        _currentWaypointNumber = -1;
        switch (currentRoad.tag)
        {
            case "RoadSimple":
                {
                    _scriptRoadSimple = currentRoad.GetComponent<ScriptRoadSimple>();
                    _currentRoadType = "Simple";
                    //print("Current road type setted.");
                    
                    break;
                }
            case "RoadSquare":
                {
                    _scriptRoadSquare = currentRoad.GetComponent<ScriptRoadSquare>();
                    _currentRoadType = "Square";
                    //print("Current road type setted.");
                    
                    break;
                }
            case "RoadTrip":
                {
                    _scriptRoadTrip = currentRoad.GetComponent<ScriptRoadTrip>();
                    _currentRoadType = "Triple";
                    //print("Current road type setted.");

                    break;
                }
            case "RoadRound":
                {
                    _scriptRoadRound = currentRoad.GetComponent<ScriptRoadRound>();
                    _currentRoadType = "Round";
                    //print("Current road type setted.");

                    break;
                }
            case "Default":
                {
                    print("Civil car script error: Get road scripts");
                    break;
                }
        }
    }

    void GetNextWaypoint()
    {
        _currentWaypointNumber += 1;
        switch (_currentRoadType)
        {
            case "Simple":
                {
                    _canMove = true;
                    //print(_scriptRoadSimple.IsThereNextWaypoint(_roadSide, _currentWaypointNumber));
                    //print(_currentWaypointNumber);
                    //print(_roadSide);
                    if (_scriptRoadSimple.IsThereNextWaypoint(_roadSide, _currentWaypointNumber))
                    {
                        if (_scriptRoadSimple.GetWaypoint(_roadSide, _currentWaypointNumber) != null)
                        {
                            _currentWaypoint = _scriptRoadSimple.GetWaypoint(_roadSide, _currentWaypointNumber);
                            ChangeNavAgentTarget();
                        }
                        
                    } else 
                    {
                        //print("Changing Road Object");
                        //print(_scriptRoadSimple.CheckIfThereIsExit(_roadSide));
                       if  (_scriptRoadSimple.CheckIfThereIsExit(_roadSide))
                        {
                            _lastRoad = _currentRoad;
                            //print("ChangedLastRoadVar");
                            _currentRoad =_scriptRoadSimple.GetExit(_roadSide);
                            GetRoadScript(_currentRoad);
                            
                            _roadSide = _scriptRoadSimple.GetNextRoadSide(_lastRoad);

                            
                        } else
                        {
                            TurnAround();
                        }
                    }
                    
                    break;
                }
            case "Square":
                {
                    _canMove = false;
                    ChooseSquareDirection();
                    GetRoadScript(_currentRoad);
                    //print("WE NEED TO CHANGE ROAD SIDE");
                    _roadSide = _scriptRoadSimple.GetNextRoadSide(_lastRoad);
                    GetNextWaypoint();
                    //ChangeNavAgentTarget();
                    break;
                }
            case "Triple":
                {
                    _canMove = false;
                    ChooseTripleDirection();
                    GetRoadScript(_currentRoad);
                    //print("WE NEED TO CHANGE ROAD SIDE");
                    _roadSide = _scriptRoadSimple.GetNextRoadSide(_lastRoad);
                    GetNextWaypoint();
                    //ChangeNavAgentTarget();
                    break;
                }
            case "Round":
                {
                    _canMove = false;
                    //if (_scriptRoadRound.ShouldTurnRight(_lastRoad))
                    //{
                    //     _currentRoad = _scriptRoadRound.GetDirectionLeft();
                    //} else
                    //{
                    //    ChooseRoundDirection();
                    //}
                    GameObject teampRoad = _currentRoad;
                    
                    _currentRoad = _scriptRoadRound.GetDirectionRight(_lastRoad);
                    _lastRoad = teampRoad;
                    GetRoadScript(_currentRoad);
                    //print("WE NEED TO CHANGE ROAD SIDE");
                    _roadSide = _scriptRoadSimple.GetNextRoadSide(_lastRoad);
                    GetNextWaypoint();
                    //ChangeNavAgentTarget();
                    break;
                }
            case "Default":
                {
                    print("Civil car script error: Get road scripts");
                    break;
                }
        }
    }

    void ChooseRoundDirection()
    {
        _roundDirectionList.Clear();
        GameObject tempGameObject;
        tempGameObject = _scriptRoadRound.GetDirectionLeft();
        _roundDirectionList.Add(tempGameObject);
        tempGameObject = null;
        tempGameObject = _scriptRoadRound.GetDirectionRight(_lastRoad);
        _roundDirectionList.Add(tempGameObject);
        DecideRoundDirection();
    }

    void DecideRoundDirection()
    {
        int tempRandomMax = _roundDirectionList.Count;
        int choosenDirection = Random.Range(0, tempRandomMax);
        //print(choosenDirection);
        _lastRoad = _currentRoad;
        _currentRoad = _roundDirectionList[choosenDirection];
        if (_currentRoad == null)
        {
            print("NULL in decideRound");
        }
    }

    void ChooseTripleDirection()
    {
        _tripleDirectionList.Clear();
        GameObject tempGameObject;
        tempGameObject = _scriptRoadTrip.GetDirectionLeft(_lastRoad);
        _tripleDirectionList.Add(tempGameObject);
        tempGameObject = null;
        tempGameObject = _scriptRoadTrip.GetDirectionRight(_lastRoad);
        _tripleDirectionList.Add(tempGameObject);

        DecideTripleDirection();
    }

    void DecideTripleDirection()
    {
        int tempRandomMax = _tripleDirectionList.Count;
        int choosenDirection = Random.Range(0, tempRandomMax);
        //print(choosenDirection);
        _lastRoad = _currentRoad;
        _currentRoad = _tripleDirectionList[choosenDirection];
        if (_currentRoad == null)
        {
            print("NULL in decideTriple");
        }
    }

    void ChooseSquareDirection()
    {
        _squareDirectionList.Clear();
        GameObject tempGameObject;
        tempGameObject = _scriptRoadSquare.GetDirectionLeft(_lastRoad);
        _squareDirectionList.Add(tempGameObject);
        tempGameObject = null;
        tempGameObject = _scriptRoadSquare.GetDirectionRight(_lastRoad);
        _squareDirectionList.Add(tempGameObject);
        tempGameObject = null;
        tempGameObject = _scriptRoadSquare.GetDirectionStrait(_lastRoad);
        _squareDirectionList.Add(tempGameObject);

        DecideSqureDirection();
    }

    void DecideSqureDirection()
    {
        int tempRandomMax = _squareDirectionList.Count;
        int choosenDirection = Random.Range(0, tempRandomMax);
        //print(choosenDirection);
        _lastRoad = _currentRoad;
        _currentRoad = _squareDirectionList[choosenDirection];
        if (_currentRoad == null )
        {
            print("NULL in decideSquare");
        }
    }

    void TurnAround()
    {
        if (_roadSide == "Left")
        { _roadSide = "Right"; } else if (_roadSide == "Right")
                                    {
                                        _roadSide = "Left"; }
        _currentWaypointNumber = -1;
    }

    void ChangeNavAgentTarget()
    {
       
        if (_agent != null)
        {
            _agent.destination = _currentWaypoint.transform.position;
        }
        //print("TargetChanged");
    }

    // Update is called once per frame
    void Update()
    {
        if (_hittedByPlayer != true)
        {
            CheckTargetDistance();
        }
        
        
    }

   public void StopAgentInCar()
    {
        _hittedByPlayer = true;
        _agent.enabled = false;

    }

    void CheckTargetDistance()
    {
        if (_canMove == true && _agent.remainingDistance < 2.0f)
        {
            //print("Car need next Waypoint");
            GetNextWaypoint();
        }
            

    }

    public bool GetAgentStatus()
    {
        return _agent.enabled;
    }

	public void GetBackToWaypoint()
	{
        if (_currentWaypoint)
        {

            _agent.SetDestination(_currentWaypoint.transform.position);
        }

	}

	public void FollowPlayer(Vector3 playerPosition)
	{   
        if (_agent.enabled)
        {
            _agent.SetDestination(playerPosition);
        }

		
	}

	public void setFollowing(bool status)
	{
		_followPlayer = status; 
	}

}

