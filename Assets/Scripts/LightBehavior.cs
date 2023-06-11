using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public static bool lightHitRatFromSource;
    float timer;
    float stopTime = 2f;
    float lightRange;



    // Start is called before the first frame update
    void Start()
    {
        lightRange = GetComponent<Light>().range;
        timer = 0;
        lightHitRatFromSource = false;

        var collider = GetComponent<CapsuleCollider>();
        collider.height = GetComponent<Light>().range;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StandRat"))
        {
            RatChaser.lightHit = true;
            lightHitRatFromSource = true;
            timer = 0;
        }
    }
}
