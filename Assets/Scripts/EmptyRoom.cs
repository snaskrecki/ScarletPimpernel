using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
      LevelGenerator.needGeneration = true;
    }
}
