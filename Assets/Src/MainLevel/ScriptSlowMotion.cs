using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptSlowMotion : MonoBehaviour {

	private float defaultPlayerSpeed;
	private float defaultTrashSpeed;
	private float defaultEnemySpeed;
	private float defaultCivilSpeed;

	void Start()
	{
		ScriptSettingsGameplay gameplay = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();
		ScriptSettingsControls controls = GameObject.Find ("Root").GetComponent<ScriptSettingsControls> ();
		defaultPlayerSpeed = controls.player_speed;
		defaultTrashSpeed = gameplay.trash_escape_speed;
		defaultCivilSpeed = gameplay.civilcar_speed;
		defaultEnemySpeed = gameplay.enemy_speed;
	}


	IEnumerator ReturnToNormal( float time, bool slowPlayer)
	{
		yield return new WaitForSeconds (time);

		ScriptPlayerControls player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		if (slowPlayer)
			player.setSpeed (defaultPlayerSpeed);
		// <- player
		ScriptTrashController trashContr = GameObject.Find ("Root").GetComponent<ScriptTrashController> ();
		List<GameObject> trash_onscene = trashContr.getTrashOnScene ();
		foreach (GameObject trash in trash_onscene) {
			trash.GetComponent<ScriptPickUpRunAway> ().setSpeed(defaultTrashSpeed);
		}
	}


	public void slowMotion(float time , bool ifPlayerSlowed , float percentToSlow)
	{
		ScriptPlayerControls player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		if (ifPlayerSlowed) {
			player.setSpeed (defaultPlayerSpeed*percentToSlow);
			player.gameObject.GetComponent<Rigidbody> ().velocity *= 0.01f;
			//Debug.Log ("slowing");
		}
		// <- player
		ScriptTrashController trashContr = GameObject.Find ("Root").GetComponent<ScriptTrashController> ();
		List<GameObject> trash_onscene = trashContr.getTrashOnScene ();
		foreach (GameObject trash in trash_onscene) {
			trash.GetComponent<ScriptPickUpRunAway> ().setSpeed(defaultTrashSpeed*percentToSlow);
		}
		// <- trash
		StartCoroutine (ReturnToNormal (time,ifPlayerSlowed));
	}
}
