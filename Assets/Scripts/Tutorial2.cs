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

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //tutorial = GameObject.FindGameObjectWithTag("Tutorial1");
        
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<ScooterController>().enabled = false;
        player.GetComponent<Abilities>().enabled = false;
        mainCamera.GetComponent<ProjectileShoot>().enabled = false;
        mainCamera.GetComponent<MouseLook>().enabled = false;
        tutorial.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        

        if(tutorial.CompareTag("Tutorial"))
        {
            tutorialText.text = "blah blah 1. use WASD keys to drive Pepper's scooter" + "\n" +
                                "2. Avoid crashing into obstacles like rats and trash cans"+ "\n" +
                                "3. If a sleeping dog is in your way press space to honk and wake it up" + "\n" +
                                "4. If a rat starts chasing you, left click to temporarily blind it";
        }
        else
        {
            tutorialText.text = " ";
        }
    }

    public void ButtonPressed()
    {
        Debug.Log("button pressed");
        //player.GetComponent<ScooterController>().enabled = true;
        ///player.GetComponent<Abilities>().enabled = true;
        mainCamera.GetComponent<ProjectileShoot>().enabled = true;
        mainCamera.GetComponent<MouseLook>().enabled = true;
        tutorial.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
