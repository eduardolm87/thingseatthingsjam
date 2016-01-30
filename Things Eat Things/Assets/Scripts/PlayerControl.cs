using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Creature possessee;

	public float power;

	Vector3 targetCamOffset;


	void Start () {
		Globals.Init();

	}
	



	void Update () {
		Globals.gCamera.GetComponent<GameCam>().LookAtThis( transform.position );
		
		if( possessee != null ){
			transform.position = possessee.transform.position;
		}


		
		if( Input.GetMouseButtonDown(0) ){
		  Ray ray = Globals.gCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		  RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000 )) {
				switch( hit.collider.gameObject.layer ){
					case Globals.kGroundLayer:
						ClickedGround( hit.point );
						break;

					case Globals.kCreaturesLayer:
						ClickedCreature( hit.collider.gameObject );
						break;

					case Globals.kSceneryLayer:
						ClickedScenery( hit.collider.gameObject );
						break;

					default:
						Debug.Log( "Clicked something unexpected..." );
						break;
				}
			}
		}

	}



	void ClickedGround( Vector3 position )
	{
		Debug.Log( "Click Ground" );
		if( possessee != null ){
			possessee.targetPos = position;
		}
	}


	void ClickedCreature( GameObject creature )
	{
		possessee = creature.GetComponent<Creature>();
		possessee.Brain.enabled = false ;
		Debug.Log( "Click Creature" );
	}


	void ClickedScenery( GameObject scenery )
	{
		Debug.Log( "Click Scenery" );
	}
}
