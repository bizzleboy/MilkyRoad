using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    public Animator animator;
    public PlayerBehavior player;
    public float duration = 5f; // change this to your animation's duration
    private int pressCount;
    private bool isQteActive;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isQteActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressCount++;
                if (pressCount >= 15)
                {
                    isQteActive = false;
                    // Player survives QTE
                }
            }
        }
    }

    public void StartQTE()
    {
        isQteActive = true;
        animator.Play("Crawl"); // replace with your animation's name
        StartCoroutine(QteCountdown());
    }

    IEnumerator QteCountdown()
    {
        yield return new WaitForSeconds(duration);
        if (isQteActive)
        {
            // Player didn't press space enough times, so they die
            player.PlayerDies();
        }
    }
}
