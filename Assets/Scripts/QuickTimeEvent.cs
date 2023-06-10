using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    public Animator enemyAnimator; // enemy animator
    public string enemyAnimationName; // animation name
    public int requiredPresses = 15; // required number of key presses
    public float animationDuration; // duration of animation

    private int currentPresses = 0; // counter for current key presses
    private bool isPlayerDead = false; // flag to indicate if player is dead
    PlayerBehavior getDeath;

    // use this for initialization
    void Start()
    {
        // start the QTE
        StartCoroutine(StartQTE());
        PlayerBehavior getDeath= GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for spacebar press and increment counter
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
        }
    }

    IEnumerator StartQTE()
    {
        // play the enemy animation
        enemyAnimator.Play(enemyAnimationName);

        // wait for the duration of the animation
        yield return new WaitForSeconds(animationDuration);

        // if the player didn't press space enough times, they die
        if (currentPresses < requiredPresses)
        {
            getDeath.PlayerDies();
        }
    }

 
}
