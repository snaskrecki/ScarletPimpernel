using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    int health;
    public Animator animator;

    private void Awake()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public int GetHealth() => health;

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void UpdateMaxHealth(int modifier)
    {
        maxHealth += modifier;
        ResetHealth();
    }

    public IEnumerator Die()
    {
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
            yield return new WaitForSeconds(1);
        }
        GameObject.Destroy(gameObject);
        // TODO show Game Over scene in next sprint.
        //Application.Quit();
    }
}
