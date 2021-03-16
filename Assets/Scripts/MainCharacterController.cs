using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

    public float speed;
    private Vector2 moveInput;

    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // transform.position += new Vector3(moveInput.x * Time.deltaTime * speed, moveInput.y * Time.deltaTime * speed, 0f);

        rigidBody.velocity = moveInput * speed;
    }
}
