using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHit : MonoBehaviour
{
    public AudioClip successHitSFX;
    public GameObject nextTarget;

    public static int count;
    public int score = 1;

    public int windowAmount;

    public bool hasBlocker = false;
    public GameObject blocker;

    private void Start()
    {
        if (hasBlocker)
        {
            blocker.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            Delivered();
        }
    }

    void Delivered()
    {
        gameObject.SetActive(false);

        if(nextTarget != null) 
        {
            nextTarget.SetActive(true);
        }

        count++;
        FindObjectOfType<LevelManager>().AddScore(score);

        if (count == windowAmount)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }

        AudioSource.PlayClipAtPoint(successHitSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
