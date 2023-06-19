using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public Slider healthSlider;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);

        transform.Rotate(-90, 0, 0, Space.Self);
    }
}
