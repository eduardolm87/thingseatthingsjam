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
	const int kWolfSong = 6;

	float LastEventTime;

	void Awake () {
	}
	
	float LastWolfAppearTime;


	void Update () {
	
		if( Input.GetKeyDown( KeyCode.X )){
				GameManager.Instance.GameEvent( "WolfAppears" );
		}

		if( !music.isPlaying ){
			StartNewMusicTrack();
		}
	}

	void StartNewMusicTrack()
	{
		float rnd = Random.value;// + GameManager.Instance.;
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
		if( Time.time - LastWolfAppearTime > 5.0f )
		{
			music.Stop();
			music.PlayOneShot( musics[ kWolfSong ] );
		}
		LastWolfAppearTime = Time.time;
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
