using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifySpeed : MonoBehaviour
{
  private Rigidbody2D body;
  public Transform ObjectPosition;
  public float speed_modifier;

  void Start()
  {
      body = GetComponent<Rigidbody2D>();
      ObjectPosition = GetComponent<Transform>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
      MainCharacterController controller = other.GetComponent<MainCharacterController>();
      if (controller != null)
      {
            controller.speed_modifier = speed_modifier;
            controller.current_modifier_timer = controller.max_modifier_timer;
            Destroy(gameObject);
      }
  }

}
