using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//first level - only loop enemies
public class OnlyLoop_Script : MonoBehaviour
{
    private int enemyList_length;

    void prepare()
    {
      enemyList_length = LevelGenerator.enemyList_length;
      LevelGenerator.enemyList_length = 1;
      // here we preapre staff for the scene
    }

    void clean_after()
    {
      LevelGenerator.enemyList_length = enemyList_length;
    }


}
