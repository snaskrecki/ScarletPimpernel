using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fourth level - more objects
public class MoreObjects_Script : MonoBehaviour
{
    void prepare()
    {
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS *= 2;
    }

    void clean_after()
    {
      //undo changes
      LevelGenerator.DEFAULT_NUMBER_OF_OBJECTS /= 2;
    }


}
