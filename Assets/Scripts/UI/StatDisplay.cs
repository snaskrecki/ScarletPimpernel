using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] Text valueText;
    

    // Update is called once per frame
    public void SetValue(int val)
    {
        valueText.text = val.ToString();
    }

    public void SetValue(float val)
    {
        valueText.text = val.ToString();
    }
}
