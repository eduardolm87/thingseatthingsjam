﻿using UnityEngine;
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
	const int kWolfGrowl = 7;
	const int kRestMus3 = 8;
	const int kRestMus4 = 9;

	float LastEventTime;

	void Awake () {
	}
	
	float LastWolfAppearTime;


	void Update () {
	
		if( Input.GetKeyDown( KeyCode.X )){
				GameManager.Instance.GameEvent( "WolfBite" );
		}

		if( !music.isPlaying ){
			StartNewMusicTrack();
		}
	}

	void StartNewMusicTrack()
	{
		float rnd = Random.value;// + GameManager.Instance.;
		if( rnd < .2f ){
			music.PlayOneShot( musics[ kRestMus1 ] );
		} else if ( rnd < .4f ){
			music.PlayOneShot(musics[ kRestMus2 ] );
		} else if ( rnd < .6f ){
			music.PlayOneShot(musics[ kRestMus3 ] );
		} else if ( rnd < .8f ){
			music.PlayOneShot(musics[ kRestMus4 ] );
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

	public void Event_WolfBite()
	{
		effects.Stop();
		effects.PlayOneShot( musics[ kWolfGrowl ] );
		effects.clip = musics[ kBirdSong ];
		effects.Play();

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
