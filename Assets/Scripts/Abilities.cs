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
    float lightRange;

    float fieldOfView = 45f;
    float timer;
    float stopTime;

    private void Start()
    {
        lightRange = lights.GetComponent<Light>().range;
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

        if (lightOn)
        {
            timer += Time.deltaTime;
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

    void StopEnemy()
    {
        
    }

    
    bool IsEnemyInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionPlayer = lights.transform.position - enemy.position;

        if (Vector3.Angle(directionPlayer, enemy.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemy.position, directionPlayer, out hit, lightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in sight");
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
