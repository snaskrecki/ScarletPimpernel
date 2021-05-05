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
        health = Mathf.Clamp(0, health + amount, maxHealth);
        Debug.Log(health + "/" + maxHealth);
    }
}
