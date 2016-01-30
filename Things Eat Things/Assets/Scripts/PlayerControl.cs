using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Creature possessee;

	public float power;

	Vector3 targetCamOffset;


	// Use this for initialization
	void Start () {
		Globals.Init();



	}
	
	// Update is called once per frame
	void Update () {
		Globals.gCamera.GetComponent<GameCam>().LookAtThis( transform.position );
		
		if( possessee != null ){
			transform.position = possessee.transform.position;
		}
	}
}
