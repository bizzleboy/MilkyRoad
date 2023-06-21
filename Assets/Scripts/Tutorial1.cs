using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject scoreText;
    public GameObject timerText;

    public static bool rulesShown = false;

    void Start()
    {
        if(!rulesShown)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.GetComponent<ScooterController>().enabled = false;
            player.GetComponent<Abilities>().enabled = false;
            mainCamera.GetComponent<MouseLook>().enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
            scoreText.SetActive(true);
            timerText.SetActive(true);
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

        rulesShown = true;
    }
}
