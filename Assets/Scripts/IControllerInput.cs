using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerInput
{
    float Vertical {get; }
    float Horizontal { get; }
    float GetTime { get; }
}
