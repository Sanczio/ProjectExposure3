
using UnityEngine;
using UnityEditor;
using System.Collections;
/*
public enum TriggerType
{
    NULL, SpawnPickUp, SpawnCar, SpawnTrash, MoveObject
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
} */

[CustomEditor(typeof(ScriptTrigger))]
public class CustomInspectorTrigger : Editor
{
    //public TriggerType _tG;
    //public CarType _carType;
    //public PickUpType _pickUpType;
    //public TrashType _trashType;

    public override void OnInspectorGUI()
    {
        ScriptTrigger targetTrigger = (ScriptTrigger)target;
        EditorGUILayout.Space();
        targetTrigger._triggerTask = (ScriptTrigger.TriggerType)EditorGUILayout.EnumPopup("Trigger type: ", targetTrigger._triggerTask);
        //ChangeTriggerType(targetTrigger);
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
        //is this one time or more?
        targetTrigger._replayable = EditorGUILayout.Toggle("Use more than once ", targetTrigger._replayable);
        //is there delay between using?
        if (targetTrigger._replayable)
            targetTrigger._replayableDelay = EditorGUILayout.Toggle("Delay between using? ", targetTrigger._replayableDelay);
        //delay between using, in seconds
        if (targetTrigger._replayableDelay && targetTrigger._replayable)
            targetTrigger._replayableDelayTime = EditorGUILayout.FloatField("Delay time between using (s): ", targetTrigger._replayableDelayTime);

        EditorGUILayout.Space();
        //Show type options
        ShowTypeOptions(targetTrigger);

       //DrawDefaultInspector();
    }

    void ShowTypeOptions(ScriptTrigger targetTrigger)
    {
        switch (targetTrigger._triggerTask)
        {
            case ScriptTrigger.TriggerType.NULL:
                
                break;
            case ScriptTrigger.TriggerType.SpawnCar:
                //Options for cars
                EditorGUILayout.LabelField("Options for Cars", EditorStyles.boldLabel);
                //Car name
                targetTrigger._carName = EditorGUILayout.TextField("Car name: ", targetTrigger._carName);
                //patroling?
                targetTrigger._patroling = EditorGUILayout.Toggle("Patroling: ", targetTrigger._patroling);
                //Car type
                targetTrigger._carTypeEnum = (ScriptTrigger.CarType)EditorGUILayout.EnumPopup("CarType: ", targetTrigger._carTypeEnum);
                ShowCarType(targetTrigger);
                //next trigger
                targetTrigger._whereToSpawnCar = EditorGUILayout.TextField("Where to spawn: ", targetTrigger._whereToSpawnCar);

                break;
            case ScriptTrigger.TriggerType.SpawnPickUp:
                //Options for cars
                EditorGUILayout.LabelField("Options for PickUp", EditorStyles.boldLabel);
                //PickUp name
                targetTrigger._pickUpName = EditorGUILayout.TextField("PickUp name: ", targetTrigger._pickUpName);
                //PickUp type
                targetTrigger._pickUpTypeEnum = (ScriptTrigger.PickUpType)EditorGUILayout.EnumPopup("PickUp type: ", targetTrigger._pickUpTypeEnum);
                ShowPickUpType(targetTrigger);
                //Where to spawn
                targetTrigger._whereToSpawnPickUp = EditorGUILayout.TextField("Where to spawn: ", targetTrigger._whereToSpawnPickUp);
                break;
            case ScriptTrigger.TriggerType.SpawnTrash:
                //Options for Trash
                EditorGUILayout.LabelField("Options for Trash Spawn", EditorStyles.boldLabel);
                //Trash name
                targetTrigger._trashName = EditorGUILayout.TextField("Trash name: ", targetTrigger._trashName);
                //Spawn type
                targetTrigger._trashTypeEnum = (ScriptTrigger.TrashType)EditorGUILayout.EnumPopup("Trash type: ", targetTrigger._trashTypeEnum);
                ShowTrashType(targetTrigger);
                //Spawn name
                targetTrigger._whereToSpawnTrash = EditorGUILayout.TextField("Spawn name: ", targetTrigger._whereToSpawnTrash);

                break;
            case ScriptTrigger.TriggerType.MoveObject:
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
        switch(targetTrigger._trashTypeEnum)
        {
            case ScriptTrigger.TrashType.A:
                targetTrigger._trashType = "A";
                break;
            case ScriptTrigger.TrashType.B:
                targetTrigger._trashType = "B";
                break;
            case ScriptTrigger.TrashType.C:
                targetTrigger._trashType = "C";
                break;
            case ScriptTrigger.TrashType.D:
                targetTrigger._trashType = "D";
                break;

        }
    }

    void ShowPickUpType(ScriptTrigger targetTrigger)
    {
        switch (targetTrigger._pickUpTypeEnum)
        {
            case ScriptTrigger.PickUpType.SpeedBoost:
                targetTrigger._pickUpType = "SpeedBoost";
                break;
            case ScriptTrigger.PickUpType.Shooting:
                targetTrigger._pickUpType = "Shooting";
                break;
        }
    }

    void ShowCarType(ScriptTrigger targetTrigger)
    {
        switch (targetTrigger._carTypeEnum)
        {
            case ScriptTrigger.CarType.CivilCar:
                targetTrigger._carType = "CivilCar";
                break;
            case ScriptTrigger.CarType.EnemyCar:
                targetTrigger._carType = "EnemyCar";
                break;
        }
    }

    void ChangeTriggerType(ScriptTrigger targetTrigger)
    {
        switch (targetTrigger._triggerTask)
        {
            case ScriptTrigger.TriggerType.NULL:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.NULL;
                break;
            case ScriptTrigger.TriggerType.SpawnCar:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnCar;
                break;
            case ScriptTrigger.TriggerType.SpawnPickUp:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnPickUp;
                break;
            case ScriptTrigger.TriggerType.SpawnTrash:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.SpawnTrash;
                break;
            case ScriptTrigger.TriggerType.MoveObject:
                targetTrigger._triggerTask = ScriptTrigger.TriggerType.MoveObject;
                break;
        }
    }
}