using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScriptPlayerHUD : MonoBehaviour {


	private int energyUsage = 0;
	private Slider energyLevel;
	private GameObject energyObject;
	private GameObject imagePrefab;
	private GameObject canvas;
	private GameObject main_text;
	private float deltaTime = 0;
	private float deltaTimeText = 0;

	public void AddEnergy(int energy)
	{
		if ( energyUsage >= 0 && energyUsage <= 100 )
			energyUsage += energy;
		energyLevel.value = energyUsage;
	}

	void Start()
	{
		energyObject = GameObject.Find("energyLevel");
		canvas = GameObject.Find ("Canvas");
		if (energyObject != null) {
			energyLevel = energyObject.GetComponent<Slider> ();
		}

		energyLevel.maxValue = 100;
		energyLevel.minValue = 0;

		imagePrefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/tutorial_image_1");
		imagePrefab = (GameObject)Instantiate (imagePrefab, new Vector2(Screen.width / 10 ,Screen.height / 10 * 7) , imagePrefab.transform.rotation);
		imagePrefab.transform.SetParent (canvas.transform, false);
		main_text = (GameObject)Resources.Load("prefabs/CanvasPrefabs/main_text");
		Text tempText = main_text.GetComponent<Text> ();
		main_text = (GameObject)Instantiate(main_text, new Vector2(Screen.width / 10 * 6 - tempText.rectTransform.rect.width /2 ,Screen.height / 10 * 2 ) , main_text.transform.rotation);
		main_text.transform.SetParent (canvas.transform, false);
	}

	void Update()
	{
		if (deltaTime > Time.time) {
			imagePrefab.GetComponent<Image> ().enabled = true;
		}else 
			imagePrefab.GetComponent<Image> ().enabled = false;
		if (deltaTimeText > Time.time) {
			main_text.GetComponent<Text> ().enabled = true;
		} else
			main_text.GetComponent<Text> ().enabled = false;
	}

	public void SpawnImage( string imgName , float time )
	{
		
		GameObject tempImagePrefab;
		tempImagePrefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/"+imgName);
		RectTransform imageSize = tempImagePrefab.GetComponent<Image> ().rectTransform;
		tempImagePrefab = (GameObject)Instantiate (tempImagePrefab, new Vector2(Screen.width / 20 * 19 - imageSize.rect.width / 2 ,Screen.height / 10 * 8 - imageSize.rect.height / 2) , tempImagePrefab.transform.rotation);
		tempImagePrefab.transform.SetParent (canvas.transform, false);
		deltaTime += time;
		imagePrefab = tempImagePrefab;
	}

	public void SpawnText ( string text , float time )
	{
		main_text.GetComponent<Text> ().text = text;
		deltaTimeText += time;
	}
}
