using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBehavior : MonoBehaviour
{
    public int damage = 20;
    public float speed = 10;

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
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

        transform.SetParent(GameObject.FindGameObjectWithTag("ProjecttileParent").transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
