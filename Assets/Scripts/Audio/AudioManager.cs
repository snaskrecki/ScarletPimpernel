using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	public Sound[] background_music;
	int current_music_id;
	
    void Awake()
    {
		current_music_id = -1;
		
		// Initializing sound sources.
		
        foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
		}
		
		foreach(Sound s in background_music)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.loop = true;
		}
    }

    public void Play(string sound_name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == sound_name);
		Debug.Log("playing: '" + sound_name + "'");
		
		if(s == null)
		{
			Debug.LogWarning("Sound '" + sound_name + "' not found");
			return;
		}
		
		s.source.Play();
	}
	
	public void PlayNextLevelBackgroundMusic()
	{
		if(current_music_id != -1)
		{
			background_music[current_music_id].source.Stop();
		}
		
		current_music_id = (current_music_id + 1) % background_music.Length;
		background_music[current_music_id].source.Play();
	}
	
	public void StopMusic()
	{
		Debug.Log("Stopping music");
		background_music[current_music_id].source.Stop();
		current_music_id = -1;
	}
}
