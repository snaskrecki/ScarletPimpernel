using UnityEngine;
public class ControllerInput : IControllerInput
{
    public float Vertical => Input.GetAxisRaw("Vertical");
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    public float GetTime => Time.deltaTime;
}
