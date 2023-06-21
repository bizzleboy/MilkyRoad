using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public GameObject sceneChange;
    public GameObject crosshair;

    public bool hasBigBuilding = false;
    public GameObject bigBuilding;

    public bool nextIsFinalLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextIsFinalLevel)
            {
                FindAnyObjectByType<LevelManager>().LevelBeat();
            }
            else
            {
                if (hasBigBuilding)
                {
                    bigBuilding.SetActive(false);
                }
                else
                {
                    sceneChange.GetComponent<DeliveryManagement>().ActivateLight();
                }

                other.GetComponent<PlayerBehavior>().ChangeToFPS();

                crosshair.SetActive(true);
                sceneChange.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
