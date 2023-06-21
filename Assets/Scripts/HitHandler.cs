using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public int hits = 6;
    public GameObject nextTarget;

    public static int currentHits;
    public AudioClip successHitSFX;

    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (!Boss.isHit)
        {
            gameObject.SetActive(false);
            currentHits = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            Hit();
        }
    }

    private void Hit()
    {
        gameObject.SetActive(false);

        if (nextTarget != null)
        {
            nextTarget.SetActive(true);
        }

        currentHits++;

        if (currentHits == hits)
        {
            // deal damage to the boss
            enemyHealth.TakeDamage(1);
            currentHits = 0;
        }

        AudioSource.PlayClipAtPoint(successHitSFX, Camera.main.transform.position);
    }
}
