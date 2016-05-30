using UnityEngine;
using System.Collections;

public class ScriptEnemyCarTrash : MonoBehaviour {

    private ScriptEnemyCarCrash _sEnemyCarCrash;
    private ScriptCivilCar _sCivilCar;
    public GameObject _trash;
    private bool _agentEnabled = true;

    private float _timeToGetTrash = 2; //get from settings 
    private float _currentGetTrashTime = 0.0f;
    private bool _carIsFull = false;
	// Use this for initialization
	void Start () {
        _sEnemyCarCrash = this.GetComponent<ScriptEnemyCarCrash>();
        _sCivilCar = this.GetComponent<ScriptCivilCar>();
        

    }
	
	// Update is called once per frame
	void Update () {
        UpdateTrashStatus();
	}

    public void setAgentStatus(bool agentStatus)
    {
        _agentEnabled = agentStatus;
    }

    void UpdateTrashStatus()
    {
        if (_agentEnabled && _currentGetTrashTime < _timeToGetTrash)
        {
            _currentGetTrashTime += 1 * Time.deltaTime;
        }
        if (_currentGetTrashTime >= _timeToGetTrash)
        {
            CarGotTrash();
        }
    }

    //Truck got trash, inform about that
    void CarGotTrash()
    {
        _carIsFull = true;
        _trash.SetActive(true);
        _sEnemyCarCrash.UpdateTrashStatus(true);
    }
}
