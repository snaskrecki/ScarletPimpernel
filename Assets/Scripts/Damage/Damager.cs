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
			audioManager.Play("PlayerHurt");
            damagable.ChangeHealth(-damage);
			
			if(damagable.GetHealth() <= 0)
			{
				audioManager.StopMusic();
				audioManager.Play("PlayerDeath");
			}
        }
    }
}
