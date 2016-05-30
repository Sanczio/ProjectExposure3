using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptPlayerHUD : MonoBehaviour {


	private int energyUsage = 0;
	private Slider energyLevel;
	private GameObject energyObject;

	public void AddEnergy(int energy)
	{
		if ( energyUsage >= 0 && energyUsage <= 100 )
			energyUsage += energy;
		energyLevel.value = energyUsage;
	}

	void Start()
	{
		energyObject = GameObject.Find("energyLevel");
		if (energyObject != null) {
			energyLevel = energyObject.GetComponent<Slider> ();
		}

		energyLevel.maxValue = 100;
		energyLevel.minValue = 0;
	}

	public void ButtonOne()
	{
		Debug.Log (" Button one pressed");
	}


}
