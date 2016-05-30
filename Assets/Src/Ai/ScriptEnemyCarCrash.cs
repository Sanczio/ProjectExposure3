using UnityEngine;
using System.Collections;

public class ScriptEnemyCarCrash : MonoBehaviour {

    private ScriptCivilCar _carScript;
    private int _hitCount = 0;
    private float _timeToDestroy = 2.0f; //Take from Settings 

    private bool _carIsFull = false;
    private ScriptEnemyCarTrash _sEnemyCarTrash;

    public GameObject _trashObject;

    
    // Use this for initialization
    void Start()
    {
        _carScript = this.GetComponent<ScriptCivilCar>();
        _sEnemyCarTrash = this.GetComponent<ScriptEnemyCarTrash>();
    }


    void OnCollisionEnter(Collision otherObject)
    {
        print(otherObject.gameObject.tag);
        if (otherObject.gameObject.tag == "Player")
        {   

            print("Hit by player");
            _hitCount++;
            if (_hitCount == 1)
            {
                StopAgent();
            }

            if (_hitCount == 1)
            {
                if (_carIsFull)
                {
                    GivePlayerReward();
                }

                DestroyCar();
            }

        }

    }

    //Stop Agent and inform about that
    void StopAgent()
    {
        _sEnemyCarTrash.setAgentStatus(false);
        _carScript.StopAgentInCar();
    }

    //Ask root to give reward for player

    void GivePlayerReward()
    {
        //give player cookie 

    }

    //Play Crash animation before this
    void DestroyCar()
    {
        Destroy(this.gameObject, _timeToDestroy);

    }

    public void UpdateTrashStatus(bool trashStatus)
    {
        _carIsFull = trashStatus;
    }

    void spawnTrash()
    {
        GameObject spawnTrash = Instantiate(_trashObject, this.transform.position, Quaternion.identity) as GameObject;
    }
}
