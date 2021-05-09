using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Rigidbody2D body;

  void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
  {
		MainCharacterController controller = other.GetComponent<MainCharacterController>();

		if(controller != null)
		{
			LevelGenerator.needGeneration = true;
		}
  }
}
