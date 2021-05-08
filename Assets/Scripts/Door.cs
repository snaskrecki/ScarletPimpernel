using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	
    void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
    {
      LevelGenerator.needGeneration = true;
    }
}
