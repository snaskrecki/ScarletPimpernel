using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    private int current_scene_number = 0;
    public static bool next_Scene = true;

    void Update()
    {
        if(!next_Scene)
          return;
        if (current_scene_number != 0)
          current_scene.clean_after();
        current_scene_number++;
        if (current_scene_number == TOTAL_NUMBER_OF_SCENES)
          //here something
        current_scene.prepare()
        LevelGenerator.needGeneration = true;
    }

}
