using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public static bool lightHitRatFromSource;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
