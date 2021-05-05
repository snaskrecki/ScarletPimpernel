using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        Debug.Log(health + "/" + maxHealth);
    }

    public int GetHealth() => health;

    public void ResetHealth()
    {
        health = maxHealth;
    }
}
