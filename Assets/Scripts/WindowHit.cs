using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHit : MonoBehaviour
{
    public AudioClip successHitSFX;
    public GameObject nextTarget;

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
        gameObject.SetActive(false);

        if(nextTarget != null) 
        {
            Debug.Log("Activated");
            nextTarget.SetActive(true);
        }

        AudioSource.PlayClipAtPoint(successHitSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }

    // public void StartActiveTarget()
    // {
    //     gameObject.SetActive(true);
    // }
}
