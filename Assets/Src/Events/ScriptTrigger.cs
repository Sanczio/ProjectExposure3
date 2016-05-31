using UnityEngine;
using System.Collections;

public class ScriptTrigger : MonoBehaviour {
    
    
    public enum TriggerType
    {
        NULL, SpawnPickUp, SpawnCar, ChangeAssigment, SpawnTrash, MoveObject
    }
    //public State pickUp = State.boost;
    public TriggerType _triggerTask = TriggerType.NULL;

    [Header("Trigger Options")]
    public bool _activateOnStart;
    public bool _onCollision;
    public bool _isThereDelay;
    public float _activeAfterDelay = 0.0f;
    public bool _isThereNextTrigger;
    public string _nextTrigger;
    
    
    
    private bool _delayGone = false;

    private bool _delayActive = false;
    private float _delayEndsTime;

    [Header("Trigger PickUp")]
    public string _pickUpName;
    public string _pickUpType;
    public string _whereToSpawnPickUp;

    [Header("Trigger Car")]
    public string _carName;
    public bool _patroling;
    public string _carType;
    public string _whereToSpawnCar;
    [Header("Trigger Assigment")]
    public string _assigmentNameID;

    [Header("Trigger Trash")]
    public string _trashName;
    public string _trashType;
    public string _whereToSpawnTrash;

    [Header("Trigger MoveObject")]
    public string _movableObjectName;
    public string _targetPlace;
    public bool _smoothMoving;
    public float _timeToSmoothMove = 0.0f;
    [Header("Trigger Effects")]
    private float temp2;




    // Use this for initialization
    void Start () {
	    if (_activateOnStart)
        {
            ActivateTrigger();
        }
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();
	}

    void UpdateTimer()
    {
        if (_delayActive && _delayEndsTime >= Time.time)
        {
            _delayGone = true;
            _delayActive = false;
        }
    }

    public void ActivateTrigger()
    {
        if (_isThereDelay == false || _delayGone)
        {
            ActionTrigger();
        } else
        {
            ActivateDelay();
        }

    }

    void ActivateDelay()
    {
        _delayEndsTime = Time.time + _activeAfterDelay;
        _delayActive = true;
    }


    //Trigger functions here
    void ActionTrigger()
    {
        switch (_triggerTask)
        {   
            //SPawn PickUp (call to PickUpSpawner)
            case TriggerType.SpawnPickUp:
                ScriptPickUpSpawner tempScript = GameObject.Find("Root").GetComponent<ScriptPickUpSpawner>();
                tempScript.CallPickUpSpawner(_pickUpType, _whereToSpawnPickUp);
                break;
            //Spawn Car (call to Car spawner)
            case TriggerType.SpawnCar:

                break;

            //Change Assigment
            case TriggerType.ChangeAssigment:

                break;

            case TriggerType.SpawnTrash:

                break;
            case TriggerType.MoveObject:

                break;
            
        }
    }

    void OnTriggerEnter(Collider collObject)
    {
        if (collObject.gameObject.tag == "Player")
        {
            ActivateTrigger();
        }
    }

    void ActivateNextTrigger()
    {
        if (_isThereNextTrigger)
        {
            ScriptTrigger tempScript = GameObject.Find("_nextTrigger").GetComponent<ScriptTrigger>();
            tempScript.ActivateTrigger();
        }
    }
}
