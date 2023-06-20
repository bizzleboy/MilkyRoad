using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public Transform ground;
    public static bool fpsMode;

    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        fpsMode = false;
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
        animate = transform.GetChild(1).GetComponent<Animator>();
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerDies();
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerDies();
            FindObjectOfType<LevelManager>().LevelLost();
        }

        if (other.CompareTag("CheeseAttack"))
        {
            GetComponent<PlayerHealth>().TakeDamage(5);
        }
    }

    public void ChangeToFPS()
    {
        GetComponent<ScooterController>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().moveToFPS();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ProjectileShoot>().enabled = true;

        transform.GetChild(1).gameObject.SetActive(false);
        fpsMode = true;
    }

    public void PlayerDies()
    {
        animate.SetTrigger("pepperDies");
    }
}
