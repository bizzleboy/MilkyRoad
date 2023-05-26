using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Replace "Obstacle" with the tag of your obstacle objects
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
