using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBlockerBehavior : MonoBehaviour
{
    public int score = 1;
    public AudioClip HitSFX;

    GameObject blocker;

    private void Start()
    {
        blocker = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            AudioSource.PlayClipAtPoint(HitSFX, Camera.main.gameObject.transform.position);
            FindObjectOfType<LevelManager>().AddScore(score);

            Destroy(blocker);
        }
    }
}
