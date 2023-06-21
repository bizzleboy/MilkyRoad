using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTutorial1 : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject scoreText;
    public GameObject timerText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.GetComponent<ScooterController>().enabled = false;
            player.GetComponent<Abilities>().enabled = false;
            mainCamera.GetComponent<MouseLook>().enabled = false;

            scoreText.SetActive(false);
            timerText.SetActive(false);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<ScooterController>().enabled = true;
        player.GetComponent<Abilities>().enabled = true;
        mainCamera.GetComponent<MouseLook>().enabled = true;
    }
}
