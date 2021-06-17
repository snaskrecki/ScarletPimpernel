using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    int health;
	bool dead;
    public Animator animator;
    public HealthBar healthBar;

    private void Awake()
    {
        health = maxHealth;
		dead = false;
    }

    void NotifyHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (animator != null && amount < 0)
        {
            animator.SetTrigger("Hurt");
        }
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if (health <= 0)
        {
            Die();
            dead = true;
        }

        NotifyHealthBar();
    }

    public int GetHealth() => health;

	public bool IsDead() => dead;

    public void ResetHealth()
    {
        health = maxHealth;

        NotifyHealthBar();
    }

    public void UpdateMaxHealth(int modifier)
    {
        maxHealth += modifier;
        ResetHealth();
    }

    public void Die()
    {
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        } else {
            GameObject.Destroy(gameObject);
        }

    }
}
