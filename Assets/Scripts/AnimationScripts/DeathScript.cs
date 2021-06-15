using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : StateMachineBehaviour
{
    float time = 0;
    MainCharacterController player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        player.Die();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time < stateInfo.length)
            time += Time.deltaTime;
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
