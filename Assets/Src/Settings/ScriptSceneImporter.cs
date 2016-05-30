using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class ScriptSceneImporter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Starting!");
        if (Load())
        { print("Great!"); }
        else print("Fail");
            

        print("Over");
	}

    //string fileName
    private bool Load()
    {
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader("Scene.txt", Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            //print("ReaderGotFile");
            using (theReader)
            {   

                // While there's lines left in the text file, do this:
                do
                {
                    //print("Function call");
                    line = theReader.ReadLine();
                    print(line);
                    if (line != null)
                    {
                        SpawnObject(line);
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        //string[] entries = line.Split(',');
                        //if (entries.Length > 0)
                       //     SpawnObject(entries);
                    }
                } while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }

    void SpawnObject(string line)
    {
        string[] entries = line.Split(',');
        string _objectName = entries[0];
        string _prefabName = entries[1];
        GameObject ObjectPrefab = (GameObject)Resources.Load("prefabs/" + _prefabName);
        
        Vector3 _objectPosition = new Vector3(float.Parse(entries[2]), float.Parse(entries[3]), float.Parse(entries[4]));
        Vector3 _objectRotation = new Vector3(float.Parse(entries[5]), float.Parse(entries[6]), float.Parse(entries[7]));
        Vector3 _objectScale = new Vector3(float.Parse(entries[8]), float.Parse(entries[9]), float.Parse(entries[10]));
        GameObject staticObject = Instantiate(ObjectPrefab, _objectPosition, Quaternion.identity) as GameObject;
        staticObject.transform.Rotate(_objectRotation);
        staticObject.transform.localScale = _objectScale;
        print("objectSpawned");
    }
}
