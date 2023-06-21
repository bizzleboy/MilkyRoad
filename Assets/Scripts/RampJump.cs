using UnityEngine;

public class RampJump : MonoBehaviour
{
    public float launchPower = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScooterController scooter = other.gameObject.GetComponent<ScooterController>();
            if (scooter != null)
            {
                scooter.LaunchUpward(launchPower);
            }
        }
    }
}
