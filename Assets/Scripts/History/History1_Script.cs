using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History1_Script : MonoBehaviour
{
    void prepare()
    {
      // here we preapre staff for the scene
    }

    void clean_after()
    {
      //undo changes

      History.next_Scene = true;
    }


}
