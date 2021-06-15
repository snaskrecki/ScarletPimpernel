using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fifth level - no demage made by player
public class NoDamage_Script : MonoBehaviour
{
    private int previousAttackPoints;
    private GameObject player;
    void prepare()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      previousAttackPoints = player.GetComponent<PlayerCombat>().attackDamage;
      player.GetComponent<PlayerCombat>().attackDamage = 0;
    }

    void clean_after()
    {
      player.GetComponent<PlayerCombat>().attackDamage = previousAttackPoints;
    }


}
