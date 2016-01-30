using UnityEngine;
using System.Collections;

public static  class Globals{

	public const float gCamTilt = 30;

	public static GameObject gCamera;
	public static GameObject gPlayer;
	
	public static void Init()
	{
		gCamera = GameObject.Find( "Camera" );
		gPlayer = GameObject.Find( "Player" );

		gCamera.transform.rotation = Quaternion.Euler( gCamTilt, 0, 0 );
	}

}

