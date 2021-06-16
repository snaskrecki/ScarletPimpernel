using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History_Script : MonoBehaviour
{
    public static  int enemyList_length;
    public static int previousAttackPoints;
    public static GameObject player;

    public static void OpenScene_prepare()
    {
      LevelGenerator.DEFAULT_NUMBER_OF_ENEMYS = 0;
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS = 0;
    }

    public static void OnlyLoop_prepare()
    {
      enemyList_length = LevelGenerator.enemyList_length;
      LevelGenerator.enemyList_length = 1;
    }

    public static void NoModifications_prepare()
    {
      // no preparations currently needed
    }

    public static void FollowSpeeded_prepare()
    {
      FollowMovementObj.modifier += 3; // 3 is quite fast here
    }

    public static void AvoidBullets_prepare()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      previousAttackPoints = player.GetComponent<PlayerCombat>().attackDamage;
      player.GetComponent<PlayerCombat>().attackDamage = 0;
    }

    public static void EndScene_prepare()
    {
      // no preparations currently needed
    }

    public static void MoreObjects_prepare()
    {
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS *= 2;
    }

    public static void OpenScene_clean()
    {
      LevelGenerator.DEFAULT_NUMBER_OF_ENEMYS = 3;
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS = 4;
    }

    public static void OnlyLoop_clean()
    {
      LevelGenerator.enemyList_length = enemyList_length;
    }

    public static void NoModifiacations_clean()
    {
      // no cleaning needed
    }

    public static void FollowSpeeded_clean()
    {
      FollowMovementObj.modifier -= 3; // 3 is quite fast here
    }

    public static void MoreObjects_clean()
    {
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS /= 2;
    }

    public static void AvoidBullets_clean()
    {
      player.GetComponent<PlayerCombat>().attackDamage = previousAttackPoints;
    }

    public static void EndScene_clean()
    {
      // no cleaning required
    }

    public static void prepare(string scene_name)
    {
        if (scene_name == "OpenScene")
        {
          OpenScene_prepare();
        }
        if (scene_name == "OnlyLoop")
        {
          OnlyLoop_prepare();
        }
        if (scene_name == "NoModifications")
        {
          NoModifications_prepare();
        }
        if (scene_name == "FollowSpeeded")
        {
          FollowSpeeded_prepare();
        }
        if (scene_name == "AvoidBullets")
        {
          AvoidBullets_prepare();
        }
        if (scene_name == "EndScene")
        {
          EndScene_prepare();
        }
        if (scene_name == "MoreObjects")
        {
          MoreObjects_prepare();
        }
    }

    public static void clean_after(string scene_name)
    {
      if (scene_name == "OpenScene")
      {
        OpenScene_clean();
      }
      if (scene_name == "OnlyLoop")
      {
        OnlyLoop_clean();
      }
      if (scene_name == "NoModifiacations")
      {
        NoModifiacations_clean();
      }
      if (scene_name == "FollowSpeeded")
      {
        FollowSpeeded_clean();
      }
      if (scene_name == "AvoidBullets")
      {
        AvoidBullets_clean();
      }
      if (scene_name == "EndScene")
      {
        EndScene_clean();
      }
      if (scene_name == "MoreObjects")
      {
        MoreObjects_clean();
      }

    }

}
