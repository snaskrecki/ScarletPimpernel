using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	
    void Awake()
    {
        foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
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
		
		Debug.Log("Found " + s.name);
		s.source.Play();
	}
}
