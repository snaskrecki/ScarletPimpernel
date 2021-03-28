using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

    public float speed = 5;
    private Vector2 moveInput;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + CalculateMovement(moveInput, speed, Time.deltaTime));
    }

    public Vector2 CalculateMovement(Vector2 coords, float speed, float time)
    {
        return new Vector2(coords.x * speed * time, coords.y * speed * time);
    }

}
