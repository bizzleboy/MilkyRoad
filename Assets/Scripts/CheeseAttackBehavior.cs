using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAttackBehavior : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5;

    Transform player;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();

        player = playerObject.transform;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Boss.isHit)
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

            transform.SetParent(GameObject.FindGameObjectWithTag("ProjecttileParent").transform);
        }
    }
}
