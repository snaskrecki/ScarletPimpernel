using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    // speed of character, should be about 3-10
    public float speed = 5;
    public float basic_speed = 5;
    public float speed_modifier = 0;
    public int max_modifier_timer = 300; // number of frames modifier works
    public int current_modifier_timer = 0;
    [SerializeField] StatDisplay speedDisplay;

    private Vector2 moveInput;

    private Rigidbody2D rigidBody;

    public IControllerInput controllerInput;

    private Animator anim;

    private bool isDead = false;
    // public static MainCharacterController instance;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (controllerInput == null)
            controllerInput = new ControllerInput();
    }

    // Update is called once per frame

    void NotifySpeedDisplay()
    {
        if (speedDisplay != null)
            speedDisplay.SetValue(speed);
    }

    void SetSpeed()
    {
        if (current_modifier_timer == max_modifier_timer)
        {
            speed = basic_speed * (1 + speed_modifier);
            current_modifier_timer--;
        }
        else
        {
            if (current_modifier_timer == 0)
            {
                speed = basic_speed;
                speed_modifier = 0;
            }
            else
            {
                if (speed_modifier != 0)
                {
                    current_modifier_timer--;
                }
            }
        }

        NotifySpeedDisplay();
    }
    void GetMoveInput()
    {
        moveInput.x = controllerInput.Horizontal;
        moveInput.y = controllerInput.Vertical;
        moveInput.Normalize();
        transform.localScale = SetFlip(transform.localScale, moveInput.x);
    }

    void Update()
    {
        if (isDead)
        {
            moveInput = Vector2.zero;
        } else {
            SetSpeed();
            GetMoveInput();
            SetAnimation();
        }
    }

    private void SetAnimation()
    {
        anim.SetFloat("Speed", moveInput.magnitude);
    }

    public void IncreaseSpeed(float speed)
    {
        basic_speed += speed;
    }

    public Vector3 SetFlip(Vector3 vect, float x)
    {
        if (x < 0)
            vect.x = -1;
        if (x > 0)
            vect.x = 1;
        return vect;
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + CalculateMovement(moveInput, speed, controllerInput.GetTime));
    }

    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        isDead = true;
        // rigidBody.simulated = false;
    }

    public Vector2 CalculateMovement(Vector2 coords, float speed, float time)
    {
        return new Vector2(coords.x * speed * time, coords.y * speed * time);
    }

}
