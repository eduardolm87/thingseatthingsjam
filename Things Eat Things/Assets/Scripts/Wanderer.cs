using UnityEngine;
using System.Collections;

public class Wanderer : MonoBehaviour {

	Creature creature;

	// Use this for initialization
	void Start () {
		creature = GetComponent<Creature>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if( Random.value < .01f )
		{
			Vector3 nuPos = transform.position;
			nuPos.x += (.5f-Random.value) * 5.0f;
			nuPos.z += (.5f-Random.value) * 3.0f;
			creature.targetPos = nuPos;
		}

	}
}
