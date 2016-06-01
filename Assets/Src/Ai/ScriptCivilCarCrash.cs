using UnityEngine;
using System.Collections;

public class ScriptCivilCarCrash : MonoBehaviour {

    private ScriptCivilCar _carScript;
    private int _hitCount = 0;
    private float _timeToDestroy = 2.0f; //Take from Settings 

	// Use this for initialization
	void Start () {
        _carScript = this.GetComponent<ScriptCivilCar>();
	}


    void OnCollisionEnter(Collision otherObject)
    {
        //print(otherObject.gameObject.tag);
        if (otherObject.gameObject.tag == "Player")
        {
            //print("Hit by player");
            _hitCount++;
            if (_hitCount == 1)
            { _carScript.StopAgentInCar(); }

            if (_hitCount == 1)
            {
                DestroyCar();
            }
            
        }

    }

    //Play Crash animation before this
    void DestroyCar()
    {
        Destroy(this.gameObject, _timeToDestroy);

    }
}
