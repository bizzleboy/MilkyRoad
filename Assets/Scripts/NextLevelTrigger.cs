using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public GameObject sceneChange;
    public GameObject crosshair;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehavior>().ChangeToFPS();

            crosshair.SetActive(true);
            sceneChange.SetActive(true);
            GameObject.FindGameObjectWithTag("SceneChange").GetComponent<DeliveryManagement>().ActivateLight();
        }
    }
}
