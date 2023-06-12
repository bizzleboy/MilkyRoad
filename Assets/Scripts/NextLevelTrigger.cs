using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public GameObject sceneChange;
    public GameObject crosshair;

    public bool hasBigBuilding = false;
    public GameObject bigBuilding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasBigBuilding)
            {
                bigBuilding.SetActive(false);
            }

            other.GetComponent<PlayerBehavior>().ChangeToFPS();

            crosshair.SetActive(true);
            sceneChange.SetActive(true);
            GameObject.FindGameObjectWithTag("SceneChange").GetComponent<DeliveryManagement>().ActivateLight();
            
            gameObject.SetActive(false);
        }
    }
}
