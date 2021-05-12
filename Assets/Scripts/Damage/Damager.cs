using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damagable = collision.gameObject.GetComponent<Damagable>();
        if(damagable != null)
        {
            damagable.ChangeHealth(-damage);
        }
    }
}
