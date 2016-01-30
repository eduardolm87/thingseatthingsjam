using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public float health;
	public float speed;
	public Vector3 targetPos;

	public MonoBehaviour behaviour;


	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

		// walk towards:
		Vector3 posOffs = targetPos - transform.position;
		if( posOffs.magnitude > .1f ){
			posOffs = posOffs.normalized * speed * Time.deltaTime;
			transform.position += posOffs;
		}

	}
}
