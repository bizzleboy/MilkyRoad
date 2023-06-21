using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStart : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;
    public float speed = 1;
    public float multiplier = 3;

    bool chasePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer)
        {
            boss.GetComponent<Animator>().SetInteger("animState", 2);
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speed = speed * multiplier;
            boss.GetComponent<Rigidbody>().useGravity = false;
            boss.GetComponent<BoxCollider>().isTrigger = true;
            chasePlayer = true;
        }
    }
}
