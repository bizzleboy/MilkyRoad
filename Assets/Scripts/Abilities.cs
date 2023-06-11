using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public AudioClip honkSFX;
    public AudioClip LigthSwitchSFX;

    public GameObject lights;
    public Transform enemy;

    bool lightOn;

    float timer;
    float stopTime = 3f;

    private void Start()
    {
        lightOn = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerBehavior.fpsMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(honkSFX, transform.position);
            }
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J))
            {
                AudioSource.PlayClipAtPoint(LigthSwitchSFX, transform.position);
                SwitchLight();
            }
        }

        if (LightBehavior.lightHitRatFromSource)
        {
            timer += Time.deltaTime;
        }

        if (timer >= stopTime)
        {
            RatChaser.lightHit = false;
            timer = 0;
        }

    }

    void SwitchLight()
    {
        if (lightOn)
        {
            lights.SetActive(false);
            lightOn = !lightOn;
        }
        else
        {
            lights.SetActive(true);
            lightOn = !lightOn;
        }
    }
}
