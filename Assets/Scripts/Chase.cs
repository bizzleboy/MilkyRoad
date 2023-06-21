using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform player;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossAppear.startChase)
        {
            Vector3 playerPosition = player.position;
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        }
    }
}
