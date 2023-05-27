using UnityEngine;

public class ScooterController : MonoBehaviour
{
    public float accelerationRate = 1f;    // Rate at which scooter accelerates
    public float maxSpeed = 10f;           // Maximum speed scooter can attain
    public float minSpeedForTurn = 2f;     // Minimum speed for scooter to be able to turn
    public float turnSpeed = 100f;         // Turning speed of scooter
    public float gravity = 9.8f;           // Gravity value

    private CharacterController characterController;  // Reference to CharacterController component
    private float speed = 0f;                         // The current speed of the scooter
    private Vector3 moveDirection = Vector3.zero;     // Direction the character will move

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float verticalInput = Input.GetAxis("Vertical");  // Assume Vertical axis for acceleration
            float horizontalInput = Input.GetAxis("Horizontal"); // Assume Horizontal axis for turning

            // Calculate acceleration and deceleration
            if (verticalInput != 0)
            {
                speed = Mathf.Clamp(speed + verticalInput * accelerationRate * Time.deltaTime, -maxSpeed, maxSpeed);
            }
            else
            {
                // decelerate smoothly when no input is given
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * accelerationRate);
            }

            // Apply turning only if speed is greater than the minimum
            if (Mathf.Abs(speed) > minSpeedForTurn)
            {
                transform.Rotate(0, horizontalInput * turnSpeed * Time.deltaTime, 0);
            }

            // Check if character controller is grounded
            if (characterController.isGrounded)
            {
                moveDirection = transform.forward * speed;
            }

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Apply the calculated movement
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            speed = 0;
        }
       
    }
}
