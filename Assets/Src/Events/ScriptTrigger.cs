using UnityEngine;
using System.Collections;

public class ScriptTrigger : MonoBehaviour {
    
    
    public enum TriggerType
    {
        NULL, SpawnPickUp, SpawnCar, SpawnTrash, MoveObject, ShowText, ShowImage
    }

    public enum CarType
    {
        EnemyCar, CivilCar
    }

    public enum TrashType
    {
        A, B, C, D
    }

    public enum PickUpType
    {
        SpeedBoost, Shooting
    }

    public enum CarRoadSide
    {
        Right, Left
    }

    public CarType _carTypeEnum;
    public PickUpType _pickUpTypeEnum;
    public TrashType _trashTypeEnum;
    public CarRoadSide _carRoadSideEnum;


    //public State pickUp = State.boost;
    public TriggerType _triggerTask = TriggerType.NULL;

    [Header("Trigger Options")]
    public bool _activateOnStart;
    public bool _onCollision;
    public bool _isThereDelay;
    public float _activeAfterDelay = 0.0f;
    public bool _isThereNextTrigger;
    public string _nextTrigger;
    public bool _replayable;
    public bool _replayableDelay;
    public float _replayableDelayTime = 0.0f;

    private bool _delayAfterDone = false;
    private bool _delayAfterActive = false;
    private float _delayAfterEndTime;

    
    private bool _delayGone = false;
    private bool _delayActive = false;
    private float _delayEndsTime;

    [Header("Trigger PickUp")]
    public string _pickUpName;
    public string _pickUpType;
    public string _whereToSpawnPickUp;

    [Header("Trigger Car")]
    public string _carName;
    public bool _carActiveAfterStart;
    public string _carType;
    public string _whereToSpawnCar;
    public string _roadSide;



    [Header("Trigger Trash")]
    public string _trashName;
    public string _trashType;
    public string _whereToSpawnTrash;

    [Header("Trigger MoveObject")]
    public string _movableObjectName;
    public string _targetPlace;
    public bool _smoothMoving;
    public float _timeToSmoothMove = 0.0f;

    [Header("Trigger Show Image")]
    public string _imageName;
    public float _imageTime;

    [Header("Trigger Show Text")]
    public string _textName;
    public float _textTime;

    private bool _triggerAlreadyActivated = false;
    




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
        //print("Current time: " + Time.time);
        //print("Delay Time: " + _delayEndsTime);
        //print("Delay Active: " + _delayActive);
        if (_delayActive && _delayEndsTime <= Time.time)
            {
                _delayGone = true;
                _delayActive = false;
                ActivateTrigger();
            }
        if (_delayAfterActive && _delayAfterEndTime <= Time.time)
        {
            _delayAfterDone = true;
            _delayAfterActive = false;
        }

    }

    public void ActivateTrigger()
    {
        if (_triggerAlreadyActivated != true)
        {
            if (_replayableDelay == false || _delayAfterDone)
            {
                //print("delayAfterDone");
                if (_isThereDelay == false || _delayGone)
                {
                    //print("ActionTrigger");
                    ActionTrigger();
                }
                else
                {
                    //print("ActivateDelay");
                    ActivateDelay();
                }
            }
            
        }
    }

    void ActivateDelayAfter()
    {
        _delayAfterEndTime = Time.time + _replayableDelayTime;
        _delayAfterActive = true;
    }

    void ActivateDelay()
    {
        _delayEndsTime = Time.time + _activeAfterDelay;
        _delayActive = true;
        //print("Delay Time: " + _delayEndsTime);
    }


    //Trigger functions here
    void ActionTrigger()
    {
        ActivateDelayAfter();
        if (_replayable != true)
        {
            _triggerAlreadyActivated = true;
        }
        switch (_triggerTask)
        {   
            //SPawn PickUp (call to PickUpSpawner)
            case TriggerType.SpawnPickUp:
                TriggerTaskSpawnPickUp();
                break;
            //Spawn Car (call to Car spawner)
            case TriggerType.SpawnCar:
                TriggerTaskSpawnCar();
                break;
            case TriggerType.SpawnTrash:
                TriggerTaskSpawnTrash();
                break;
            case TriggerType.MoveObject:
                TriggerTaskMoveObject();
                break;
            case TriggerType.ShowImage:
                TriggerShowImage();
                break;
            case TriggerType.ShowText:
                TriggerShowText();
                break;

        }
        ActivateNextTrigger();
    }

    void TriggerShowImage()
    {
        ScriptPlayerHUD tempHud = GameObject.Find("Root").GetComponent<ScriptPlayerHUD>();
        tempHud.SpawnImage(_imageName, _imageTime);
    }

    void TriggerShowText()
    {
        ScriptPlayerHUD tempHud = GameObject.Find("Root").GetComponent<ScriptPlayerHUD>();
        tempHud.SpawnText(_textName, _textTime);
    }

    void TriggerTaskSpawnTrash()
    {
        ScriptTrashController tempTrashScript = GameObject.Find("Root").GetComponent<ScriptTrashController>();
        tempTrashScript.spawnTrash(_trashName, _whereToSpawnTrash, _trashType);

    }


    void TriggerTaskMoveObject()
    {
        GameObject movableObject = GameObject.Find(_movableObjectName);
        GameObject targetPlace = GameObject.Find(_targetPlace);
        if (_smoothMoving)
        {
            movableObject.transform.position = Vector3.Lerp(movableObject.transform.position, targetPlace.transform.position, Mathf.SmoothStep(0.0f, 1.0f, _timeToSmoothMove));
        } else
            {
            
            movableObject.transform.position = targetPlace.transform.position;
            }
    }

    void TriggerTaskSpawnCar()
    {
        ScriptCarSpawner tempCarSpawnerScript = GameObject.Find("Root").GetComponent<ScriptCarSpawner>();
        tempCarSpawnerScript.CallCarSpawner(_carName, _carType, _whereToSpawnCar, _roadSide, _carActiveAfterStart);
    }



    void TriggerTaskSpawnPickUp()
    {
        ScriptPickUpSpawner tempScript = GameObject.Find("Root").GetComponent<ScriptPickUpSpawner>();
        tempScript.CallPickUpSpawner(_pickUpType, _whereToSpawnPickUp);
    }

    void OnTriggerEnter(Collider collObject)
    {
        if (_onCollision && collObject.gameObject.tag == "TriggerSphere")
        {
            ActivateTrigger();
        }
    }

    void ActivateNextTrigger()
    {
        if (_isThereNextTrigger)
        {
            ScriptTrigger tempScript = GameObject.Find(_nextTrigger).GetComponent<ScriptTrigger>();
            tempScript.ActivateTrigger();
        }
    }

}
