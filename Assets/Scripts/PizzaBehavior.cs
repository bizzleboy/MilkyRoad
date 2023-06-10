using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBehavior : MonoBehaviour
{
    public int score = 1;
    public AudioClip pickupSFX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.gameObject.transform.position);
            FindObjectOfType<LevelManager>().AddScore(score);
            Destroy(gameObject);
        }
    }

}
