using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public AudioSource effects;
	public AudioSource music;

	public AudioClip[] musics;
	const int kBirdSong = 2;
	const int kChangeForm = 1;
	const int kRestMus1 = 3;
	const int kRestMus2 = 4;
	const int kMidMus3 = 5;

	float LastEventTime;

	void Awake () {
	}
	


	void Update () {
	
//		if( Input.GetKeyDown( KeyCode.X )){
//			effects.clip = musics[ kBirdSong ];	
//		}

		if( !music.isPlaying ){
			StartNewMusicTrack();
		}
	}

	void StartNewMusicTrack()
	{
		float rnd = Random.value;
		if( rnd < .4f ){
			music.PlayOneShot( musics[ kRestMus1 ] );
		} else
 		if ( rnd < .8f ){
			music.PlayOneShot(musics[ kRestMus2 ] );
		} else {
			music.PlayOneShot(musics[ kMidMus3 ] );
		}

	}


	public void Event_WolfAppears()
	{
	}

	public void Event_HunterAppears()
	{
	}

	public void Event_StartGame()
	{
		effects.clip = musics[ kBirdSong ];
		effects.Play();

	}

	public void Event_ChangeForm()
	{
		effects.PlayOneShot( musics[ kChangeForm ]);
	}

	
	
}
