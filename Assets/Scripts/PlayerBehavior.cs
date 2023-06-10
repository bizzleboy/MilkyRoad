using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform ground;
    public static bool fpsMode;

    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").transform;
        animate = transform.GetChild(1).GetComponent<Animator>();
        fpsMode = false;
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
            PlayerDies();
            FindObjectOfType<LevelManager>().LevelLost();
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
