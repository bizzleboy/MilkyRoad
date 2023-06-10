using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagement : MonoBehaviour
{
    public GameObject primaryLight;
    public GameObject secondaryLight;
    public void ActivateLight()
    {
        secondaryLight.SetActive(true);
        primaryLight.SetActive(false);
    }
}
