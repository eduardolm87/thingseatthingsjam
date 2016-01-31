using UnityEngine;
using System.Collections;

public class Glowy : MonoBehaviour {

	public float Frequency;

	public SpriteRenderer backSprite;
	public SpriteRenderer frontSprite;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float fade = .5f + .3f * Mathf.Sin( Time.time * Frequency );
		
		backSprite.color = new Color( 1, 1, 1, fade );
		frontSprite.color = new Color( 1,1,1, 1.0f - fade );
	}
}
