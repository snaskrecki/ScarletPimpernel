using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playGameButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void exitGameButton()
    {
        Application.Quit();
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
