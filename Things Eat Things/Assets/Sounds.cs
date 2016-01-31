using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	AudioSource sound;
	public AudioClip[] musics;
	const int kChangeForm = 1;

	float LastEventTime;

	void Awake () {
		sound = GetComponent<AudioSource>();
	}
	


	void Update () {
	
	}

	public void Event_WolfAppears()
	{
	}

	public void Event_HunterAppears()
	{
	}

	public void Event_GameStarts()
	{
//		sound.clip;
	}

	public void Event_ChangeForm()
	{
		sound.PlayOneShot( musics[ kChangeForm ]);
	}

	
	
}
