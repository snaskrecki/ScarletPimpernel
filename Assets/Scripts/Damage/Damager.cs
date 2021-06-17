using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;
	AudioManager audioManager;
	
	void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damagable = collision.gameObject.GetComponent<Damagable>();
        if (damagable != null)
        {
			if(damagable.GetHealth() - damage <= 0)
			{
				StopMusic();
				PlaySound("PlayerDeath");
			}
			else
			{
				PlaySound("PlayerHurt");
			}
			
            damagable.ChangeHealth(-damage);
        }
    }
	
	private void PlaySound(string name)
	{
		if(audioManager != null)
		{
			audioManager.Play(name);
		}
	}
	
	private void StopMusic()
	{
		if(audioManager != null)
		{
			audioManager.StopMusic();
		}
	}
}
