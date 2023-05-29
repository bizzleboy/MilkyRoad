using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honking : MonoBehaviour
{
    public AudioClip honkSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(honkSFX, transform.position);
        }
    }
}
