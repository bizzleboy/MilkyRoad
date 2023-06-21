using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour
{
    public GameObject player;
    public GameObject tutorial;
    public GameObject mainCamera;
    public Text tutorialText;
    public static bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            player.GetComponent<ScooterController>().enabled = false;
            player.GetComponent<Abilities>().enabled = false;
            mainCamera.GetComponent<MouseLook>().enabled = false;
            tutorial.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            if (tutorial.CompareTag("Tutorial"))
            {
                tutorialText.text = "1. Use WASD keys to drive Pepper's scooter" + "\n" +
                                    "2. Avoid crashing into obstacles like rats and trash cans" + "\n" +
                                    "3. Use Shift to drift \n" +
                                    "3. If a sleeping dog is in your way press space to honk and wake it up" + "\n" +
                                    "4. If a rat starts chasing you, left click to temporarily blind it";
            }
            else
            {
                tutorialText.text = " ";
            }

            triggered = true;
        }
        
    }

    public void ButtonPressed()
    {
        player.GetComponent<ScooterController>().enabled = true;
        player.GetComponent<Abilities>().enabled = true;
        mainCamera.GetComponent<MouseLook>().enabled = true;
        tutorial.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
