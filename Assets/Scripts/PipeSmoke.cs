using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSmoke : MonoBehaviour
{
    public int blastDuration = 2;
    public int blastInterval = 4;
    public GameObject smokeParticles;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        InvokeRepeating("StartSmoke", 0, blastInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSmoke()
    {
        smokeParticles.SetActive(true);
        GetComponent<Collider>().enabled = true;
        StartCoroutine(BlowSmoke());
    }

    IEnumerator BlowSmoke()
    {
        yield return new WaitForSeconds(blastDuration);
        smokeParticles.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}
