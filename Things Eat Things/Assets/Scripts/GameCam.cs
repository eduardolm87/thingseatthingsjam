using UnityEngine;
using System.Collections;

public class GameCam : MonoBehaviour {

	public float camSpeed;
	Vector3 targetLookPos;
	public float dist;

	// Use this for initialization
	void Start () {

	}
	

	// Update is called once per frame
	void Update () {
		
	}


	void FixedUpdate()
	{
		Vector3 offset = new Vector3( 0, Mathf.Sin( -Mathf.Deg2Rad*Globals.gCamTilt ), Mathf.Cos( -Mathf.Deg2Rad*Globals.gCamTilt ) );
		offset *= dist;
		
		Vector3 targetPos = targetLookPos - offset;
		float interp = Mathf.Clamp( Time.deltaTime * camSpeed, 0, 1 );
		Vector3 nuPos = transform.position * (1.0f-interp) + targetPos * (interp);
		transform.position = nuPos;
	}


	public void LookAtThis( Vector3 inPos ) {
		targetLookPos = inPos;
	}

}
