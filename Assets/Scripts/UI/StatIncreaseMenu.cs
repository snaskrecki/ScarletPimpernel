using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIncreaseMenu : MonoBehaviour
{
    public GameObject statIncreaseUI;
    public void Pause()
    {
        statIncreaseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        statIncreaseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
