using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedBoostAmount = 5f;
    public float speedBoostDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        ScooterController scooter = other.GetComponent<ScooterController>();

        if (scooter != null)
        {
            scooter.BoostSpeed(speedBoostAmount, speedBoostDuration);
        }
    }
}
