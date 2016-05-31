
using UnityEngine;
using UnityEditor;
using System.Collections;

public enum TriggerType
{
    NULL, SpawnPickUp, SpawnCar, ChangeAssigment, SpawnTrash, MoveObject
}

public enum CarType
{
    EnemyCar,CivilCar 
}

public enum TrashType
{
    A, B, C, D
}

public enum PickUpType
{
    SpeedBoost, Shooting
}

[CustomEditor(typeof(ScriptTrigger))]
public class CustomInspectorTrigger : Editor
{
    public TriggerType _tG;
    public CarType _carType;
    public PickUpType _pickUpType;
    public TrashType _trashType;

    public override void OnInspectorGUI()
    {
        ScriptTrigger targetTrigger = (ScriptTrigger)target;
        EditorGUILayout.Space();
        _tG = (TriggerType)EditorGUILayout.EnumPopup("Trigger type: ", _tG);

        ChangeTriggerType(targetTrigger);
        EditorGUILayout.Space();

        //Standart Options for trigger
        EditorGUILayout.LabelField("Standart Options for Trigger", EditorStyles.boldLabel);
        //active on start
        targetTrigger._activateOnStart = EditorGUILayout.Toggle("Active on Start", targetTrigger._activateOnStart);
        //on collision
        targetTrigger._onCollision = EditorGUILayout.Toggle("On collision", targetTrigger._onCollision);
        //is there delay?
        targetTrigger._isThereDelay = EditorGUILayout.Toggle("Delay before start", targetTrigger._isThereDelay);
        //active after delay
        if(targetTrigger._isThereDelay)
            targetTrigger._activeAfterDelay = EditorGUILayout.FloatField("Delay time (s): ", targetTrigger._activeAfterDelay);
        //Is there next trigger
        targetTrigger._isThereNextTrigger = EditorGUILayout.Toggle("Activate next trigger ", targetTrigger._isThereNextTrigger);
        //next trigger
        if (targetTrigger._isThereNextTrigger)
            targetTrigger._nextTrigger = EditorGUILayout.TextField("Next trigger name: ", targetTrigger._nextTrigger);

        EditorGUILayout.Space();
        //Show type options
        ShowTypeOptions(targetTrigger);

       //DrawDefaultInspector();
    }



    void ShowTypeOptions(ScriptTrigger targetTrigger)
    {
        switch (_tG)
        {
            case TriggerType.NULL:
                
                break;
            case TriggerType.SpawnCar:
                //Options for cars
                EditorGUILayout.LabelField("Options for Cars", EditorStyles.boldLabel);
                //Car name
                targetTrigger._carName = EditorGUILayout.TextField("Car name: ", targetTrigger._carName);
                //patroling?
                targetTrigger._patroling = EditorGUILayout.Toggle("Patroling: ", targetTrigger._patroling);
                //Car type
                _carType = (CarType)EditorGUILayout.EnumPopup("CarType: ", _carType);
                ShowCarType(targetTrigger);
                //next trigger
                targetTrigger._whereToSpawnCar = EditorGUILayout.TextField("Where to spawn: ", targetTrigger._whereToSpawnCar);

                break;
            case TriggerType.SpawnPickUp:
                //Options for cars
                EditorGUILayout.LabelField("Options for PickUp", EditorStyles.boldLabel);
                //PickUp name
                targetTrigger._pickUpName = EditorGUILayout.TextField("PickUp name: ", targetTrigger._pickUpName);
                //PickUp type
                _pickUpType = (PickUpType)EditorGUILayout.EnumPopup("PickUp type: ", _pickUpType);
                ShowPickUpType(targetTrigger);
                //Where to spawn
                targetTrigger._whereToSpawnPickUp = EditorGUILayout.TextField("Where to spawn: ", targetTrigger._whereToSpawnPickUp);
                break;
            case TriggerType.SpawnTrash:
                //Options for Trash
                EditorGUILayout.LabelField("Options for Trash Spawn", EditorStyles.boldLabel);
                //Trash name
                targetTrigger._trashName = EditorGUILayout.TextField("Trash name: ", targetTrigger._trashName);
                //Spawn type
                _trashType = (TrashType)EditorGUILayout.EnumPopup("Trash type: ", _trashType);
                ShowTrashType(targetTrigger);
                //Spawn name
                targetTrigger._whereToSpawnTrash = EditorGUILayout.TextField("Spawn name: ", targetTrigger._whereToSpawnTrash);

                break;
            case TriggerType.ChangeAssigment:
                
                break;
            case TriggerType.MoveObject:
                //Options for MovingObject
                EditorGUILayout.LabelField("Options for Moving Object", EditorStyles.boldLabel);
                //Movable object name
                targetTrigger._movableObjectName = EditorGUILayout.TextField("Movable object name: ", targetTrigger._movableObjectName);
                //Target place name
                targetTrigger._targetPlace = EditorGUILayout.TextField("Target Place name: ", targetTrigger._targetPlace);
                //smooth moving?
                targetTrigger._smoothMoving = EditorGUILayout.Toggle("Smooth moving?: ", targetTrigger._smoothMoving);
                //time to smooth move
                if (targetTrigger._smoothMoving) targetTrigger._timeToSmoothMove = EditorGUILayout.FloatField("Smooth moving time: ", targetTrigger._timeToSmoothMove);
                break;
            
        }
    }

    void ShowTrashType(ScriptTrigger targetTrigger)
    {
        switch(_trashType)
        {
            case TrashType.A:
                targetTrigger._trashType = "A";
                break;
            case TrashType.B:
                targetTrigger._trashType = "B";
                break;
            case TrashType.C:
                targetTrigger._trashType = "C";
                break;
            case TrashType.D:
                targetTrigger._trashType = "D";
                break;

        }
    }

    void ShowPickUpType(ScriptTrigger targetTrigger)
    {
        switch (_pickUpType)
        {
            case PickUpType.SpeedBoost:
                targetTrigger._pickUpType = "SpeedBoost";
                break;
            case PickUpType.Shooting:
                targetTrigger._pickUpType = "Shooting";
                break;
        }
    }

    void ShowCarType(ScriptTrigger targetTrigger)
    {
        switch (_carType)
        {
            case CarType.CivilCar:
                targetTrigger._carType = "CivilCar";
                break;
            case CarType.EnemyCar:
                targetTrigger._carType = "EnemyCar";
                break;
        }
    }

    void ChangeTriggerType(ScriptTrigger targetTrigger)
    {
        switch (_tG)
        {
            case TriggerType.NULL:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.NULL;
                break;
            case TriggerType.SpawnCar:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnCar;
                break;
            case TriggerType.SpawnPickUp:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnPickUp;
                break;
            case TriggerType.SpawnTrash:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnTrash;
                break;
            case TriggerType.ChangeAssigment:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.ChangeAssigment;
                break;
            case TriggerType.MoveObject:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.MoveObject;
                break;
        }
    }
}