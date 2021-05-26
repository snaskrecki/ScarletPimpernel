using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealth : MonoBehaviour
{
  private Rigidbody2D body;
  public Transform ObjectPosition;
  public int healthModifier;

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
            other.GetComponent<Damagable>().ChangeHealth(healthModifier);
            Destroy(gameObject);
      }
  }

}
