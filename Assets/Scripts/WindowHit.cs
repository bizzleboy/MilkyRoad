using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            Delivered();
        }
    }

    void Delivered()
    {
        Instantiate(dementorExpelled, transform.position, transform.rotation);

        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
