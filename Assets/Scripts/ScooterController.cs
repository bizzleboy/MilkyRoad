using UnityEngine;

public class ScooterController : MonoBehaviour
{
    public float accelerationRate = 1f;
    public float maxSpeed = 10f;
    public float minSpeedForTurn = 2f;
    public float turnSpeed = 100f;
    public float gravity = 9.8f;

    public float rampForceMultiplier = 5f; // Force multiplier for ramp launch

    private CharacterController characterController;
    private float speed = 0f;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            if (verticalInput != 0)
            {
                speed = Mathf.Clamp(speed + verticalInput * accelerationRate * Time.deltaTime, -maxSpeed, maxSpeed);
            }
            else
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * accelerationRate);
            }

            if (Mathf.Abs(speed) > minSpeedForTurn)
            {
                transform.Rotate(0, horizontalInput * turnSpeed * Time.deltaTime, 0);
            }

            if (characterController.isGrounded)
            {
                moveDirection = transform.forward * speed;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            speed = 0;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("RampEnd")) // tag the ramp end with "RampEnd"
        {
            moveDirection.y = speed * rampForceMultiplier; // adjust the launch force
        }
    }
}
