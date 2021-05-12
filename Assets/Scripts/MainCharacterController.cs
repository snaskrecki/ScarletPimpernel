using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    // speed of character, should be about 3-10
    public float speed = 5;

    private Vector2 moveInput;

    private Rigidbody2D rigidBody;

    public IControllerInput controllerInput;

   // public static MainCharacterController instance;

    void Awake()
    {
     //   instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (controllerInput == null)
            controllerInput = new ControllerInput();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = controllerInput.Horizontal;
        moveInput.y = controllerInput.Vertical;
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + CalculateMovement(moveInput, speed, controllerInput.GetTime));
    }

    public Vector2 CalculateMovement(Vector2 coords, float speed, float time)
    {
        return new Vector2(coords.x * speed * time, coords.y * speed * time);
    }

}
