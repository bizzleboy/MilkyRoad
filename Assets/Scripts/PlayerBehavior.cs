using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform ground;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            if (transform.position.y < ground.position.y)
            {
                FindObjectOfType<LevelManager>().LevelLost();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Replace "Obstacle" with the tag of your obstacle objects
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }
}
