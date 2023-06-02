using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkedDog : MonoBehaviour
{
    public Transform player;
    public float distanceBetweenPlayerAndDog = 15;
    public float movementSpeed = 3;

    Animator animate;
    bool honked;
    bool walk;
    bool rotate;

    float timer = 0;

    public Direction direction;

    public enum Direction
    {
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        animate = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        honked = false;
        walk = false;
        rotate = false;

        direction = Direction.Right;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= distanceBetweenPlayerAndDog)
                {
                    animate.SetInteger("dogState", 1);
                    honked = true;
                    
                }
            }
        }

        if (honked)
        {
            timer += Time.deltaTime;
            walk = true;
        }

        if (timer >= 1 && walk)
        {
            if (!rotate)
            {
                if (direction == Direction.Right)
                {
                    transform.Rotate(new Vector3(0, 90, 0));
                }
                else if (direction == Direction.Left)
                {
                    transform.Rotate(new Vector3(0, -90, 0));
                }
                
                rotate = true;
            }
            animate.SetInteger("dogState", 2);
            walk = false;
        }

        if (timer >= 3)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !honked)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }
}
