using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagement : MonoBehaviour
{
    public GameObject primaryLight;
    public GameObject secondaryLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLight()
    {
        secondaryLight.SetActive(true);
        primaryLight.SetActive(false);
    }
}
