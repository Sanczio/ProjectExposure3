using UnityEngine;
using System.Collections;

public class ScriptActiveRunAway : MonoBehaviour {

    public GameObject _parent; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.tag == "bio_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }
        if (collidingObject.tag == "recycable_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }


    }

    void OnTriggerStay(Collider collidingObject)
    {
        if (collidingObject.tag == "bio_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }
        if (collidingObject.tag == "recycable_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }

    }

    void ActivatePickupRunAway(Collider targetObject)
    {
		if ( targetObject.gameObject != null){
			ScriptPickUpRunAway tempScript = targetObject.GetComponent<ScriptPickUpRunAway>();
			tempScript.ActivatePickUp(_parent.transform);
		} 
    }
}
