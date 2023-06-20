using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    public GameObject boss;
    public float stunDistance = 10;
    public GameObject Explosion;
    public Transform player;
    public float explosionTime = 8;
    public AudioClip explosionSFX;

    float explodeTimer;
    float distanceToBoss;
    bool explode;
    

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explode = false;
        explodeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToBoss = Vector3.Distance(transform.position, boss.transform.position);
        transform.LookAt(player);

        if (explode)
        {
            if (distanceToBoss <= stunDistance)
            {
                Boss.isHit = true;
                Boss.turnOnFirstHit = true;
            }
            Explode();
        }

        explodeTimer += Time.deltaTime;

        if (explodeTimer >= explosionTime)
        {
            Explode();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            explode = true;
        }
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        Instantiate(Explosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        Destroy(gameObject, 2f);
    }
}
