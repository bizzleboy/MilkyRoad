using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    public GameObject boss;
    public float stunDistance = 20;
    public GameObject Explosion;
    public Transform player;

    float distanceToBoss;
    bool explode;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explode = false;
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
            }
            gameObject.SetActive(false);
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject, 2f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            explode = true;
        }
    }
}
