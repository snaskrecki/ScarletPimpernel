using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Third level - follow enemies speeded up
public class FollowSpeeded_Script : MonoBehaviour
{
    void prepare()
    {
        FollowMovementObj.modifier += 3; // 3 is quite fast here
    }

    void clean_after()
    {
      FollowMovementObj.modifier -=3;
    }


}
