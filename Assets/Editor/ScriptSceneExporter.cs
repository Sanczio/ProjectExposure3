using UnityEngine;
using System.Collections;

using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class ScriptSceneExporter : EditorWindow
{



    [MenuItem("Window/My Custom Tools/Export Scene")]
    static void exportScene()
    {
        string path = "Scene.txt";
        TextWriter f = new StreamWriter(path);

        //Get all static objects
        Object[] _listSceneStaticObjects = FindObjectsOfType(typeof(ScriptStaticObject));
        foreach (object obj in _listSceneStaticObjects)
        {
            ScriptStaticObject sceneObject = (ScriptStaticObject)obj;
            Quaternion rotation = sceneObject.GetComponent<Transform>().rotation;
            f.WriteLine(sceneObject.name + "," + sceneObject._Prefab.name+ "," + sceneObject.transform.position.x + ","+ sceneObject.transform.position.y + "," + sceneObject.transform.position.z + "," + rotation.eulerAngles.x + "," + rotation.eulerAngles.y + "," + rotation.eulerAngles.z + "," + sceneObject.transform.localScale.x + "," + sceneObject.transform.localScale.y + "," + sceneObject.transform.localScale.z);
            //f.WriteLine(rotation.eulerAngles.x + "," + rotation.eulerAngles.y + "," + rotation.eulerAngles.z);
        }


        f.Close();
    }



}