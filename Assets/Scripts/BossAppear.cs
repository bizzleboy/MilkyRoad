using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppear : MonoBehaviour
{
    public static bool startChase;
    public GameObject boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startChase = true;

            boss.SetActive(true);
        }
    }
}
