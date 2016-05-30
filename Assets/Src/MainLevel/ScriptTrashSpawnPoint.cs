using UnityEngine;
using System.Collections;

public class ScriptTrashSpawnPoint : MonoBehaviour {

	private bool available = true;
	private int areaIn = 0;

	public bool checkIfAvailable()
	{
		return available;
	}

	public void makeAvailable(bool available_)
	{
		if (available_)
			available = true;
		else
			available = false;
	}

	public int getAreaIn()
	{
		return areaIn;
	}

	public void setAreaIn( int num)
	{
		areaIn = num;
	}





}
