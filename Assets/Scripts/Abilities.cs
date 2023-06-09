using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public AudioClip honkSFX;
    public AudioClip LigthSwitchSFX;

    public GameObject lights;
    public Slider lightSlider;

    public float decreasePower = 0.2f;
    public float increasePower = 0.1f;
    public bool hasLight = false;
    public float maxLightPower = 100;

    bool lightOn;

    float timer;
    float lightPower;
    float stopTime = 2f;

    private void Start()
    {
        lightOn = false;
        timer = 0;
        lightPower = maxLightPower;
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
            if (hasLight)
            {
                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J))
                {
                    AudioSource.PlayClipAtPoint(LigthSwitchSFX, transform.position);
                    SwitchLight();
                }
            }
            
        }

        if (hasLight)
        {
            lightSlider.value = lightPower;
            HandleLightSlider();

            HandleTimer();
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

    void HandleLightSlider()
    {
        if (lightOn)
        {
            lightPower -= decreasePower;
            if (lightPower <= 0)
            {
                lightOn = true;
                SwitchLight();
            }
        }
        else
        {
            lightPower += increasePower;
            if (lightPower >= maxLightPower)
            {
                lightPower = maxLightPower;
            }
        }
    }

    void HandleTimer()
    {
        if (LightBehavior.lightHitRatFromSource)
        {
            timer = 0;
            RatChaser.lightHit = true;
            LightBehavior.lightHitRatFromSource = false;
        }

        if (RatChaser.lightHit)
        {
            timer += Time.deltaTime;
        }

        if (timer >= stopTime)
        {
            RatChaser.lightHit = false;
            timer = 0;
        }
    }
}
